using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Contact.UserContact
{
    public class CreateOrEditUserContactDto: FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public string Note { get; set; }
        
        public bool IsActive { get; set; }
    }
}