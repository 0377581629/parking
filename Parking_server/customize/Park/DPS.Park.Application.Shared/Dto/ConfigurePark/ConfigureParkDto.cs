namespace DPS.Park.Application.Shared.Dto.ConfigurePark
{
    public class ConfigureParkDto
    {
        #region AdminPage
        public string PhoneToSendMessage { get; set; }

        public int BalanceToSendEmail { get; set; }

        #endregion
        
        #region Base Info
        public string Name { get; set; }
        
        public string Hotline { get; set; }
        
        public string Address { get; set; }
        
        public string SubAddress1 { get; set; }
        
        public string SubAddress2 { get; set; }
        
        public string Email { get; set; }
        #endregion
    }
}