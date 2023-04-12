namespace DPS.Park.Application.Shared.Dto.ConfigurePark
{
    public class ConfigureParkDto
    {
        #region AdminPage

        public bool ApplyDecreasePercent { get; set; }
        
        public int? DecreasePercent { get; set; }
        
        public string PhoneToSendMessage { get; set; }
        
        public int TotalSlotCount { get; set; }

        public int BalanceToSendEmail { get; set; }

        #endregion
        
        #region Base Info
        public string Name { get; set; }
        
        public string Hotline { get; set; }
        #endregion
    }
}