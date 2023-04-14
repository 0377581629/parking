using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Order;

namespace DPS.Park.Application.Shared.Interface.Common
{
    public interface IParkPublicAppService: IApplicationService
    {
        #region User
        Task<PagedResultDto<GetOrderForViewDto>> GetMyOrders(ParkPublicInput input);

        #endregion
    }
}