using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DPS.Park.Core.Message
{
    [Table("Park_Message")]
    public class Message: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string Content { get; set; }
    }
}