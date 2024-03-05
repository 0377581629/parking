using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Vehicle.VehicleType
{
    public class GetAllVehicleTypeInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}