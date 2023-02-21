using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;

namespace DPS.Park.Application.Shared.Interface.Vehicle
{
    public interface IVehicleTypeAppService: IApplicationService 
    {
        Task<PagedResultDto<GetVehicleTypeForViewDto>> GetAll(GetAllVehicleTypeInput input);
        
        Task<GetVehicleTypeForEditOutput> GetVehicleTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditVehicleTypeDto input);

        Task Delete(EntityDto input);
    }
}