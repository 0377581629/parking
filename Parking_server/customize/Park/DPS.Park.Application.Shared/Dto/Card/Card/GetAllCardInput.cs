using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Card.Card
{
    public class GetAllCardInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}