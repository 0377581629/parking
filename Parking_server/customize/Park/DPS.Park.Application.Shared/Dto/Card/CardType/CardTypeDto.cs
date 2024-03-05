using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Card.CardType
{
    public class CardTypeDto: FullAuditedEntityDto
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
    }
}