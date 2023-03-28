using Abp.Domain.Services;

namespace DPS.Park.Application.Shared.BackgroundJobs
{
    public interface IEmailStudentOutOfMoneyBackGroundJob: IDomainService
    {
        void SendEmailStudentOutOfMoney();
    }
}