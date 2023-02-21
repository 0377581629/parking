using System;
using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.History
{
    public class CreateOrEditHistoryDto: FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public string CardCode { get; set; }
        public string LicensePlate { get; set; }
        
        public double Price { get; set; }
        
        public DateTime InTime { get; set; }
        
        public DateTime OutTime { get; set; }
        
        public int? CardTypeId { get; set; }
        
        public string CardTypeName { get; set; }

        public int? VehicleTypeId { get; set; }
        
        public string VehicleTypeName { get; set; }
    }
}