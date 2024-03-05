using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DPS.Park.Core.Card;
using JetBrains.Annotations;

namespace DPS.Park.Core.Order
{
    [Table("Park_Order_Order")]
    public class Order: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string Code { get; set; }
        
        public int? CardId { get; set; }
        
        [ForeignKey("CardId")]
        [CanBeNull]
        public Card.Card Card { get; set; }
        
        public double Amount { get; set; }
        
        public int Status { get; set; }
        
        public long VnpTransactionNo { get; set; }
    }
}