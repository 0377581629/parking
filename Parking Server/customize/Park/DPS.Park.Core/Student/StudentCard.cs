using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace DPS.Park.Core.Student
{
    [Table("Parking_Student_StudentCard")]
    public class StudentCard: Entity,IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public int StudentId { get; set; }
        
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        
        public int CardId { get; set; }
        
        [ForeignKey("CardId")]
        public virtual Card.Card Card { get; set; }
        
        public string Note { get; set; }
    }
}