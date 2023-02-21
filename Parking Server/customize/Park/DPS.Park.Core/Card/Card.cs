using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DPS.Park.Core.Vehicle;
using JetBrains.Annotations;

namespace DPS.Park.Core.Card
{
    [Table("Park_Card_Card")]
    public class Card: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string Code { get; set; }
        
        public string CardNumber { get; set; }
        
        public string Note { get; set; }
        
        public bool IsActive { get; set; }
        
        public int? CardTypeId { get; set; }
        
        [ForeignKey("CardTypeId")]
        [CanBeNull]
        public CardType CardType { get; set; }
        
        public int? VehicleTypeId { get; set; }
        
        [ForeignKey("VehicleTypeId")]
        [CanBeNull]
        public VehicleType VehicleType { get; set; }
    }
}