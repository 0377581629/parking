using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using DPS.Park.Core.Shared;

namespace DPS.Park.Application.Shared.Dto.Organization.WorkDepartment
{
    public class CreateOrEditWorkDepartmentDto: EntityDto<long?>
    {
        public int? TenantId { get; set; }
        
        public long? ParentId { get; set; }

        public int WorkGroupId { get; set; }
        
        [Required]
        [StringLength(ParkConsts.MaxCodeLength, MinimumLength = ParkConsts.MinCodeLength)]
        public string DepartmentCode { get; set; }
        
        [Required]
        [StringLength(ParkConsts.MaxStrLength, MinimumLength = ParkConsts.MinStrLength)]
        public string DisplayName { get; set; }
        
        public string Describe { get; set; }
    }
}