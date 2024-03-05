using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using DPS.Reporting.Application.Shared.Dto.Student;

namespace DPS.Reporting.Application.Shared.Interfaces
{
    public interface IStudentReportingAppService: IApplicationService
    {
        Task<List<StudentRenewCardReportingOutput>> StudentRenewCardReport(StudentRenewCardReportingInput input);
    }
}