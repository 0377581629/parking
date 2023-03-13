using System.Linq.Dynamic.Core;
using System.Net.Mail;
using System.Text;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Threading;
using DPS.Park.Application.Shared.BackgroundJobs;
using DPS.Park.Core.Resident;
using DPS.Park.Core.Shared;
using Zero;
using Zero.Configuration;
using Zero.Customize;

namespace DPS.Park.Application.BackgroundJobs
{
    public class EmailResidentNotPaidBackGroundJob : ZeroDomainServiceBase, IEmailResidentNotPaidBackGroundJob
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<ResidentCard> _residentCardRepository;

        public EmailResidentNotPaidBackGroundJob(IUnitOfWorkManager unitOfWorkManager, IAbpSession abpSession,
            IRepository<Resident> residentRepository, IRepository<EmailTemplate> emailTemplateRepository,
            IEmailSender emailSender, ISettingManager settingManager, IRepository<ResidentCard> residentCardRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _abpSession = abpSession;
            _residentRepository = residentRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _emailSender = emailSender;
            _settingManager = settingManager;
            _residentCardRepository = residentCardRepository;
        }

        public void SendEmailResidentNotPaid()
        {
            using var unitOfWork = _unitOfWorkManager.Begin();

            var residentNotPaid = AsyncHelper.RunSync(() =>
                _residentRepository.GetAllListAsync(o =>
                    !o.IsDeleted && o.TenantId == _abpSession.TenantId && o.IsPaid == false));

            var emailTemplate = AsyncHelper.RunSync(() =>
                _emailTemplateRepository.FirstOrDefaultAsync(o =>
                    !o.IsDeleted && o.IsActive && o.TenantId == _abpSession.TenantId &&
                    o.EmailTemplateType == (int?) ZeroEnums.EmailTemplateType.ResidentNotPaid));

            var monthlyFare = AsyncHelper.RunSync(() => _settingManager.GetSettingValueAsync<int>(AppSettings
                .ParkSettings
                .MonthlyFare));

            if (emailTemplate == null) return;

            //Email
            foreach (var resident in residentNotPaid)
            {
                var residentCardCount = AsyncHelper.RunSync(() =>
                    _residentCardRepository.CountAsync(o =>
                        o.TenantId == _abpSession.TenantId && o.ResidentId == resident.Id));

                var totalPaid = residentCardCount * monthlyFare * 1000;

                var mailBody = new StringBuilder();
                mailBody.Append(emailTemplate.Content);
                mailBody.Replace("{OwnerFullName}", resident.OwnerFullName);
                mailBody.Replace("{ApartmentNumber}", resident.ApartmentNumber);
                mailBody.Replace("{TotalPaid}", totalPaid.ToString());
                _emailSender.SendAsync(new MailMessage()
                {
                    To = {resident.OwnerEmail},
                    Subject = emailTemplate.Title,
                    Body = mailBody.ToString(),
                    IsBodyHtml = true
                });
            }

            unitOfWork.CompleteAsync();
        }
    }
}