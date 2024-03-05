using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Vehicle.VehicleType
{
    public class CreateOrEditVehicleTypeDto: FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
    }
}