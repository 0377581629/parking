using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Resident.ResidentCard
{
    public class ResidentCardDto: EntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public int ResidentId { get; set; }
        
        public string ApartmentNumber { get; set; }
        
        public string OwnerFullName { get; set; }
        
        public string OwnerEmail { get; set; }
        
        public string OwnerPhone { get; set; }
        
        public int CardId { get; set; }
        
        public string CardCode { get; set; }
        
        public string CardNumber { get; set; }
        
        public string Note { get; set; }
    }
}