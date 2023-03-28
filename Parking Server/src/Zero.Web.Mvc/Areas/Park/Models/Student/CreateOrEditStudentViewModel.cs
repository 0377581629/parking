using DPS.Park.Application.Shared.Dto.Student;

namespace Zero.Web.Areas.Park.Models.Student
{
    public class CreateOrEditStudentViewModel
    {
        public CreateOrEditStudentDto Student { get; set; }

        public bool IsEditMode => Student.Id.HasValue;
    }
}