using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using DPS.Reporting.Application.Shared.Dto.Resident;

namespace DPS.Reporting.Application.Shared.Interfaces
{
    public interface IResidentReportingAppService: IApplicationService
    {
        Task<List<ResidentPaidReportingOutput>> ResidentPaidReport(ResidentPaidReportingInput input);
    }
}