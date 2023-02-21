using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DPS.Park.Core.Shared;

namespace DPS.Park.Core.Base
{
    public class BaseDictionaryEntity: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        [StringLength(ParkConsts.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ParkConsts.MaxNameLength, MinimumLength = ParkConsts.MinNameLength)]
        public string Name { get; set; }

        public string Note { get; set; }
        
        public bool IsActive { get; set; }
    }
}