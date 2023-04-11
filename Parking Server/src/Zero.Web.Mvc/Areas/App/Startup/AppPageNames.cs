namespace Zero.Web.Areas.App.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string Dashboard = "Dashboard";
            public const string EmailTemplate = "Administration.EmailTemplate";

            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";
            public const string WebhookSubscriptions = "Administration.WebhookSubscriptions";
            public const string DynamicProperties = "Administration.DynamicProperties";
            public const string DynamicEntityProperties = "Administration.DynamicEntityProperties";

            public const string FilesManager = "Administration.FilesManager";
        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";

            public const string DashboardWidget = "DashboardWidget";
            public const string CurrencyRate = "CurrencyRate";

            public const string HangfireDashboard = "HangfireDashboard";
        }

        public static class Tenant
        {
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }

        public static class Cms
        {
            public const string Settings = "Cms.Settings";

            public const string ImageBlockGroup = "Cms.ImageBlockGroup";

            public const string ImageBlock = "Cms.ImageBlock";

            public const string Widget = "Cms.Widget";

            public const string Page = "Cms.Page";

            public const string PageLayout = "Cms.PageLayout";

            public const string PageTheme = "Cms.PageTheme";

            public const string Tags = "Cms.Tags";

            public const string MenuGroup = "Cms.MenuGroup";

            public const string Menu = "Cms.Menu";

            public const string Category = "Cms.Category";

            public const string Post = "Cms.Post";
        }

        public static class Park
        {
            public const string ConfigurePark = "Park.ConfigurePark";

            public const string History = "Park.History";

            public const string CardMenu = "Park.CardMenu";

            public const string CardType = "Park.CardType";

            public const string Card = "Park.Card";

            public const string VehicleType = "Park.VehicleType";

            public const string Fare = "Park.Fare";

            public const string StudentMenu = "Park.StudentMenu";

            public const string Student = "Park.Student";
            
            public const string OrderMenu = "Park.OrderMenu";
            
            public const string Order = "Park.Order";
        }

        public static class Reporting
        {
            public const string Reports = "Reporting.Reports";

            #region Student

            public const string StudentReports = "Reporting.StudentReports";
            public const string StudentRenewCardReports = "Reporting.StudentRenewCardReports";

            #endregion
            
            
        }
    }
}