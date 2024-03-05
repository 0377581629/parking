using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Order;

namespace DPS.Park.Application.Shared.Interface.Order
{
    public interface IOrderAppService : IApplicationService
    {
        Task<PagedResultDto<GetOrderForViewDto>> GetAll(GetAllOrderInput input);

        Task<GetOrderForEditOutput> GetOrderForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditOrderDto input);

        Task Delete(EntityDto input);
    }
}