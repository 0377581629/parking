using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Card.Card
{
    public class CardDto: FullAuditedEntityDto
    {
        public int? TenantId { get; set; }
        
        public string Code { get; set; }
        
        public string CardNumber { get; set; }
        
        public string Note { get; set; }
        
        public bool IsActive { get; set; }
        
        public int? CardTypeId { get; set; }
        
        public string CardTypeName { get; set; }

        public int? VehicleTypeId { get; set; }
        
        public string VehicleTypeName { get; set; }
        
        public int Balance { get; set; }
        
        public double Price { get; set; } 
    }
}