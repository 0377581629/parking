using DPS.Park.Application.Shared.Dto.Card.Card;

namespace Zero.Web.Areas.Park.Models.Card
{
    public class CreateOrEditCardViewModel
    {
        public CreateOrEditCardDto Card { get; set; }

        public bool IsEditMode => Card.Id.HasValue;
    }
}