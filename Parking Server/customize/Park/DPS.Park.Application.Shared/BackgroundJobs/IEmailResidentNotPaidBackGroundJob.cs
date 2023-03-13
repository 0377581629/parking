using Abp.Domain.Services;

namespace DPS.Park.Application.Shared.BackgroundJobs
{
    public interface IEmailResidentNotPaidBackGroundJob: IDomainService
    {
        void SendEmailResidentNotPaid();
    }
}