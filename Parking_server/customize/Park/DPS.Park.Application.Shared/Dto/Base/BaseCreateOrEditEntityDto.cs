using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using DPS.Park.Core.Shared;

namespace DPS.Park.Application.Shared.Dto.Base
{
	public class BaseCreateOrEditEntityDto : EntityDto<int?>
	{
		public int? TenantId { get; set; }
		
		[Required]
		[StringLength(ParkConsts.MaxCodeLength, MinimumLength = ParkConsts.MinCodeLength)]
		public string Code { get; set; }
		
		[Required]
		[StringLength(ParkConsts.MaxNameLength, MinimumLength = ParkConsts.MinNameLength)]
		public string Name { get; set; }

		public string Note { get; set; }
		
		public bool IsActive { get; set; }
	}
}