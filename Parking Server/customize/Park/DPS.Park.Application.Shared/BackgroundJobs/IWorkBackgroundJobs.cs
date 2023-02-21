using Abp.Domain.Services;

namespace DPS.Park.Application.Shared.BackgroundJobs
{
    public interface IWorkBackgroundJobs : IDomainService
    {
        void ApplyChangeWork();
        
        void ApplyChangeWorkChild();
    }
}