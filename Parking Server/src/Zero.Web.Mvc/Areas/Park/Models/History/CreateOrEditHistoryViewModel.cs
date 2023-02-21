using DPS.Park.Application.Shared.Dto.History;

namespace Zero.Web.Areas.Park.Models.History
{
    public class CreateOrEditHistoryViewModel
    {
        public CreateOrEditHistoryDto History { get; set; }

        public bool IsEditMode => History.Id.HasValue;
    }
}