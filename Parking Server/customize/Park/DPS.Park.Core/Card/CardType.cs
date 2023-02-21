using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DPS.Park.Core.Card
{
    [Table("Park_Card_CardType")]
    public class CardType: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
    }
}