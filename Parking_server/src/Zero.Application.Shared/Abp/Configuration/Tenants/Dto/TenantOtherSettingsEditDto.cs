namespace Zero.Configuration.Tenants.Dto
{
    public class TenantOtherSettingsEditDto
    {
        public bool IsQuickThemeSelectEnabled { get; set; }

        #region Extent

        public string ReportLeftHeader { get; set; }
        
        public string ReportRightHeader { get; set; }

        #endregion
    }
}