using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DPS.Park.Core.Card;
using DPS.Park.Core.Vehicle;
using JetBrains.Annotations;

namespace DPS.Park.Core.History
{
    [Table("Park_History")]
    public class History: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public int? CardId { get; set; }
        
        [ForeignKey("CardId")]
        [CanBeNull]
        public Card.Card Card { get; set; }
        
        public string LicensePlate { get; set; }
        
        [CanBeNull]
        public double? Price { get; set; }
        
        public DateTime Time { get; set; }
        
        public int Type { get; set; }
        
        public string Photo { get; set; }
    }
}