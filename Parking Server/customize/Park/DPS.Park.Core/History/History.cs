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
        
        public string CardCode { get; set; }
        public string LicensePlate { get; set; }
        
        public double Price { get; set; }
        
        public DateTime InTime { get; set; }
        
        public DateTime OutTime { get; set; }
        
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