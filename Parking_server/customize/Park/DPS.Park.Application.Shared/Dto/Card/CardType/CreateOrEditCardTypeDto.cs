using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Card.CardType
{
    public class CreateOrEditCardTypeDto: FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
    }
}