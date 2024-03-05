using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Student
{
    public class GetAllStudentInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}