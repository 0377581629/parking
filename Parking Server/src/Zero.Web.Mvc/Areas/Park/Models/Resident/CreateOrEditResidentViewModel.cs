using DPS.Park.Application.Shared.Dto.Resident;

namespace Zero.Web.Areas.Park.Models.Resident
{
    public class CreateOrEditResidentViewModel
    {
        public CreateOrEditResidentDto Resident { get; set; }

        public bool IsEditMode => Resident.Id.HasValue;
    }
}