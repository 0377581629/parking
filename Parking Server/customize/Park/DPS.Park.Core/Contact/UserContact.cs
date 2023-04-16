using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Zero;

namespace DPS.Park.Core.Contact
{
    [Table("Park_Contact_UserContact")]
    public class UserContact: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        [Required]
        [StringLength(ZeroConst.MaxNameLength, MinimumLength = ZeroConst.MinNameLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ZeroConst.MaxNameLength, MinimumLength = ZeroConst.MinNameLength)]
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }
        
        public string Note { get; set; }
        
        public bool IsActive { get; set; }
    }
}