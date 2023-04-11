using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Order
{
    public class GetAllOrderInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}