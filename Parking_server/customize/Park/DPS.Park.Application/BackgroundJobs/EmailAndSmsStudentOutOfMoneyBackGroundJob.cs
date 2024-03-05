using System.Linq;
using System.Net.Mail;
using System.Text;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Threading;
using DPS.Park.Application.Shared.BackgroundJobs;
using DPS.Park.Core.Card;
using DPS.Park.Core.Student;
using Zero;
using Zero.Abp.Net.Sms;
using Zero.Configuration;
using Zero.Customize;
using Zero.Net.Sms;

namespace DPS.Park.Application.BackgroundJobs
{
    public class EmailAndSmsStudentOutOfMoneyBackGroundJob : ZeroDomainServiceBase, IEmailAndSmsStudentOutOfMoneyBackGroundJob
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<StudentCard> _studentCardRepository;
        private readonly IRepository<Card> _cardRepository;
        private readonly ISmsSender _smsSender;

        public EmailAndSmsStudentOutOfMoneyBackGroundJob(IUnitOfWorkManager unitOfWorkManager, IAbpSession abpSession,
            IRepository<Student> studentRepository, IRepository<EmailTemplate> emailTemplateRepository,
            IEmailSender emailSender, ISettingManager settingManager, IRepository<StudentCard> studentCardRepository,
            IRepository<Card> cardRepository, ISmsSender smsSender)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _abpSession = abpSession;
            _studentRepository = studentRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _emailSender = emailSender;
            _settingManager = settingManager;
            _studentCardRepository = studentCardRepository;
            _cardRepository = cardRepository;
            _smsSender = smsSender;
        }

        public void SendEmailAndSmsStudentOutOfMoney()
        {
            using var unitOfWork = _unitOfWorkManager.Begin();

            var balanceToSend = AsyncHelper.RunSync(() =>
                _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings.BalanceToSendEmail));

            var cardsOutOfMoney = AsyncHelper.RunSync(() =>
                _cardRepository.GetAllListAsync(o =>
                    !o.IsDeleted && o.IsActive && o.TenantId == _abpSession.TenantId &&
                    o.Balance < balanceToSend));

            var students = AsyncHelper.RunSync(() =>
                _studentRepository.GetAllListAsync(
                    o => !o.IsDeleted && o.IsActive && o.TenantId == _abpSession.TenantId));
            var studentCards = AsyncHelper.RunSync(() => _studentCardRepository.GetAllListAsync());

            var emailTemplate = AsyncHelper.RunSync(() =>
                _emailTemplateRepository.FirstOrDefaultAsync(o =>
                    !o.IsDeleted && o.IsActive && o.TenantId == _abpSession.TenantId &&
                    o.EmailTemplateType == (int?)ZeroEnums.EmailTemplateType.StudentOutOfMoney));

            foreach (var cardOutOfMoney in cardsOutOfMoney)
            {
                var studentCard = studentCards.FirstOrDefault(o => o.CardId == cardOutOfMoney.Id);
                var student = students.FirstOrDefault(o => studentCard != null && o.Id == studentCard.StudentId);

                if (student == null) continue;

                if (emailTemplate != null && !string.IsNullOrEmpty(student.Email))
                {
                    var mailBody = new StringBuilder();
                    mailBody.Append(emailTemplate.Content);
                    mailBody.Replace("{StudentCode}", student.Code);
                    mailBody.Replace("{StudentName}", student.Name);
                    mailBody.Replace("{CardNumber}", cardOutOfMoney.CardNumber);
                    mailBody.Replace("{Balance}", cardOutOfMoney.Balance.ToString());
                    _emailSender.SendAsync(new MailMessage()
                    {
                        To = { student.Email },
                        Subject = emailTemplate.Title,
                        Body = mailBody.ToString(),
                        IsBodyHtml = true
                    });
                }

                if (!string.IsNullOrEmpty(student.PhoneNumber))
                {
                    var message =
                        $"Chào em {student.Name}, MSSV {student.Code}, hiện nay thẻ xe có số thẻ là {cardOutOfMoney.CardNumber} của em đã sắp hết chỉ còn {cardOutOfMoney.Balance.ToString()} đồng. Chúng tôi thông báo để em có thể biết trước và chủ động nạp thêm.";
                    _smsSender.SendAsync(student.PhoneNumber, message);
                }
            }
            
            unitOfWork.CompleteAsync();
            }
        }
    }