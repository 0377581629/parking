using System;
using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Student
{
    public class StudentDto: FullAuditedEntityDto<int>
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
    }
}