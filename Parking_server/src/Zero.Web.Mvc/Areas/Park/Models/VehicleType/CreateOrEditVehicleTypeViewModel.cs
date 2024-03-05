using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;

namespace Zero.Web.Areas.Park.Models.VehicleType
{
    public class CreateOrEditVehicleTypeViewModel
    {
        public CreateOrEditVehicleTypeDto VehicleType { get; set; }

        public bool IsEditMode => VehicleType.Id.HasValue;
    }
}