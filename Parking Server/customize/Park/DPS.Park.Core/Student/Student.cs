using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DPS.Park.Core.Student
{
    [Table("Parking_Student_Student")]
    public class Student : FullAuditedEntity, IMayHaveTenant
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