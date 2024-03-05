using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Message
{
    public class CreateOrEditMessageDto: FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public string Content { get; set; }
    }
}