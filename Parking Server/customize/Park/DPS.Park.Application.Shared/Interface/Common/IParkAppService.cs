using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Card.CardType;
using DPS.Park.Application.Shared.Dto.Fare;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;

namespace DPS.Park.Application.Shared.Interface.Common
{
    public interface IParkAppService: IApplicationService
    {
        Task<List<VehicleTypeDto>> GetAllVehicleTypes();

        Task<PagedResultDto<VehicleTypeDto>> GetPagedVehicleTypes(GetAllVehicleTypeInput input);
        
        Task<List<CardTypeDto>> GetAllCardTypes();

        Task<PagedResultDto<CardTypeDto>> GetPagedCardTypes(GetAllCardTypeInput input);
        
        Task<List<FareDto>> GetAllFares();
    }
}