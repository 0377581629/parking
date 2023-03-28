using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Student.StudentCard;

namespace DPS.Park.Application.Shared.Dto.Student
{
    public class CreateOrEditStudentDto : FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }

        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Avatar { get; set; }
        
        public string Email { get; set; }
        
        public bool Gender { get; set; }
        
        public DateTime DoB { get; set; }
        
        public bool IsActive { get; set; }

        public List<StudentCardDto> StudentDetails { get; set; }
    }
}