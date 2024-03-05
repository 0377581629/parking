using Abp.Domain.Services;

namespace DPS.Park.Application.Shared.BackgroundJobs
{
    public interface IEmailAndSmsStudentOutOfMoneyBackGroundJob: IDomainService
    {
        void SendEmailAndSmsStudentOutOfMoney();
    }
}