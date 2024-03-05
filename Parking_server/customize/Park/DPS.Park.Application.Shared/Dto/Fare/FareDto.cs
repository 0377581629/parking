using System;
using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Fare
{
    public class FareDto: FullAuditedEntityDto
    {
        public int? TenantId { get; set; }
        
        public int? CardTypeId { get; set; }
        
        public string CardTypeName { get; set; }

        public int? VehicleTypeId { get; set; }
        
        public string VehicleTypeName { get; set; }

        public double Price { get; set; }
        
        public int Type { get; set; }
    }
}