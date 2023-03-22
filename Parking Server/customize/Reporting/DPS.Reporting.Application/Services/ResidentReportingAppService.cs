using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using DPS.Park.Core.Resident;
using DPS.Reporting.Application.Shared.Dto.Resident;
using DPS.Reporting.Application.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;
using Zero.Configuration;

namespace DPS.Reporting.Application.Services
{
    [AbpAuthorize(ReportPermissions.ResidentReports)]
    public class ResidentReportingAppService : ZeroAppServiceBase, IResidentReportingAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Resident> _residentRepository;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<ResidentCard> _residentCardRepository;

        public ResidentReportingAppService(IAbpSession abpSession, IRepository<Resident> residentRepository,
            ISettingManager settingManager, IRepository<ResidentCard> residentCardRepository)
        {
            _abpSession = abpSession;
            _residentRepository = residentRepository;
            _settingManager = settingManager;
            _residentCardRepository = residentCardRepository;
        }

        [AbpAuthorize(ReportPermissions.ResidentPaidReport)]
        public async Task<List<ResidentPaidReportingOutput>> ResidentPaidReport(ResidentPaidReportingInput input)
        {
            var monthlyFare = await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings.MonthlyFare);

            var residentCard = await _residentCardRepository.GetAllListAsync(o => o.TenantId == _abpSession.TenantId);

            var residentsNotPaid = await _residentRepository.GetAll().Where(o =>
                !o.IsDeleted && o.IsActive && o.TenantId == _abpSession.TenantId && !o.IsPaid).ToListAsync();

            var res = new List<ResidentPaidReportingOutput>();

            foreach (var resident in residentsNotPaid)
            {
                var residentCardCount = residentCard.Count(o => o.ResidentId == resident.Id);
                res.Add(new ResidentPaidReportingOutput()
                {
                    ApartmentNumber = resident.ApartmentNumber,
                    OwnerFullName = resident.OwnerFullName,
                    MoneyToPay = residentCardCount * monthlyFare * 1000,
                    IsPaid = resident.IsPaid ? L("IsPaid") : L("IsNotPaid"),
                });
            }

            return res;
        }
    }
}