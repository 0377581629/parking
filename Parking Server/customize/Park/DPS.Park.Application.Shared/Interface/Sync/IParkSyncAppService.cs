using System.Threading.Tasks;
using DPS.Park.Application.Shared.Dto.Sync;

namespace DPS.Park.Application.Shared.Interface.Sync
{
    public interface IParkSyncAppService
    {
        Task SendInfo(SyncDto input);

        Task<BaseInfoDto> GetInfo();

        Task<MessageInfoDto> SendMessageInfo();
    }
}