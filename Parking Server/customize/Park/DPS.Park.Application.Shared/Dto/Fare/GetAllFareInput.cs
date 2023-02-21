using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Fare
{
    public class GetAllFareInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}