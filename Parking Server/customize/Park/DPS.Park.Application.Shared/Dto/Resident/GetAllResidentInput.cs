using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Resident
{
    public class GetAllResidentInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}