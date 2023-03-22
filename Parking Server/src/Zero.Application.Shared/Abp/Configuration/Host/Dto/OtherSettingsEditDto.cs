namespace Zero.Configuration.Host.Dto
{
    public class OtherSettingsEditDto
    {
        public bool IsQuickThemeSelectEnabled { get; set; }
        
        #region Extent
        public string ReportLeftHeader { get; set; }
        
        public string ReportRightHeader { get; set; }
        #endregion
    }
}