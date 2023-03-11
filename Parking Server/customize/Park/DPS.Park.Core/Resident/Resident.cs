using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DPS.Park.Core.Resident
{
    [Table("Parking_Resident_Resident")]
    public class Resident : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string ApartmentNumber { get; set; }
        
        public string OwnerFullName { get; set; }
        
        public string OwnerEmail { get; set; }
        
        public string OwnerPhone { get; set; }
        
        public bool IsPaid { get; set; }
        
        public bool IsActive { get; set; }
    }
}