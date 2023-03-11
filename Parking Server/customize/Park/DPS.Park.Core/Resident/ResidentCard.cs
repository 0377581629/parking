using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace DPS.Park.Core.Resident
{
    [Table("Parking_Resident_ResidentCard")]
    public class ResidentCard: Entity,IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public int ResidentId { get; set; }
        
        [ForeignKey("ResidentId")]
        public virtual Resident Resident { get; set; }
        
        public int CardId { get; set; }
        
        [ForeignKey("CardId")]
        public virtual Card.Card Card { get; set; }
        
        public string Note { get; set; }
    }
}