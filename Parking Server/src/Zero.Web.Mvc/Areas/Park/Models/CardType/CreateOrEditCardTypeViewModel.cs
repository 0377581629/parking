using DPS.Park.Application.Shared.Dto.Card.CardType;

namespace Zero.Web.Areas.Park.Models.CardType
{
    public class CreateOrEditCardTypeViewModel
    {
        public CreateOrEditCardTypeDto CardType { get; set; }

        public bool IsEditMode => CardType.Id.HasValue;

    }
}