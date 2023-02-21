using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.History;

namespace DPS.Park.Application.Shared.Interface.History
{
    public interface IHistoryAppService: IApplicationService 
    {
        Task<PagedResultDto<GetHistoryForViewDto>> GetAll(GetAllHistoryInput input);
        
        Task<GetHistoryForEditOutput> GetHistoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditHistoryDto input);

        Task Delete(EntityDto input);
    }
}