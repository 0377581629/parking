using System.Collections.Generic;
using System.Threading.Tasks;
using DPS.Park.Application.Shared.Dto.Sync;

namespace DPS.Park.Application.Shared.Interface.Sync
{
    public interface IParkSyncAppService
    {
        Task SendInfo(SyncDto input);

        Task<List<GetStudentActiveInfoSyncDto>> GetStudentActiveInfo();

        Task<MessageInfoDto> SendMessageInfo();
    }
}