namespace DPS.Park.Application.Shared.Dto.ConfigurePark
{
    public class ConfigureParkDto
    {
        public bool ApplyDecreasePercent { get; set; }
        
        public int? DecreasePercent { get; set; }
        
        public string PhoneToSendMessage { get; set; }
        
        public int TotalSlotCount { get; set; }
    }
}