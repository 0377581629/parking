using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Organization.WorkDepartment
{
    public class GetAllWorkDepartmentInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
        
    }
}