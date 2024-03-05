using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Order
{
    public class OrderDto: FullAuditedEntityDto<int>
    {
        public int? TenantId { get; set; }
        
        public string Code { get; set; }
        
        public int? CardId { get; set; }
        
        public string CardCode { get; set; }
        
        public string CardNumber { get; set; }
        
        public double Amount { get; set; }
        
        public int Status { get; set; }
        
        public long VnpTransactionNo { get; set; }
    }
}