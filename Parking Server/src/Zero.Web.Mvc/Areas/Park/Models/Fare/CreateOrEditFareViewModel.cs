using DPS.Park.Application.Shared.Dto.Fare;

namespace Zero.Web.Areas.Park.Models.Fare
{
    public class CreateOrEditFareViewModel
    {
        public CreateOrEditFareDto Fare { get; set; }

        public bool IsEditMode => Fare.Id.HasValue;
    }
}