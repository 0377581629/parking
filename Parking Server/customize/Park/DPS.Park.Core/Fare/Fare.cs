using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DPS.Park.Core.Card;
using DPS.Park.Core.Vehicle;
using JetBrains.Annotations;

namespace DPS.Park.Core.Fare
{
    [Table("Park_Fare")]
    public class Fare: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public int? CardTypeId { get; set; }
        
        [ForeignKey("CardTypeId")]
        [CanBeNull]
        public CardType CardType { get; set; }
        
        public int? VehicleTypeId { get; set; }
        
        [ForeignKey("VehicleTypeId")]
        [CanBeNull]
        public VehicleType VehicleType { get; set; }
        
        public double Price { get; set; }
        
        public int DayOfWeekStart { get; set; }
        
        public int DayOfWeekEnd { get; set; }
    }
}