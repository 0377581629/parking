using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Fare;

namespace DPS.Park.Application.Shared.Interface.Fare
{
    public interface IFareAppService: IApplicationService 
    {
        Task<PagedResultDto<GetFareForViewDto>> GetAll(GetAllFareInput input);
        
        Task<GetFareForEditOutput> GetFareForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditFareDto input);

        Task Delete(EntityDto input);
    }
}