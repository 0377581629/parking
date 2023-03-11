using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Resident
{
    public class ResidentDto: FullAuditedEntityDto<int>
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