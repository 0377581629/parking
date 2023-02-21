using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Card.CardType
{
    public class GetAllCardTypeInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}