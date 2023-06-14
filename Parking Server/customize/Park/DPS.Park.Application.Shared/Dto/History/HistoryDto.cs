using System;
using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.History
{
    public class HistoryDto: FullAuditedEntityDto
    {
        public int? TenantId { get; set; }
        
        public int? CardId { get; set; }
        public string CardCode { get; set; }
        
        public string CardNumber { get; set; }
        
        public string LicensePlate { get; set; }
        
        public double? Price { get; set; }
        
        public DateTime Time { get; set; }
        
        public int Type { get; set; }
        
        public string Photo { get; set; }
        
        public string CardTypeName { get; set; }
        
        public string VehicleTypeName { get; set; }
    }
}