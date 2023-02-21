using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace DPS.Park.Core.Vehicle
{
    [Table("Park_Vehicle_VehicleType")]
    public class VehicleType: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
    }
}