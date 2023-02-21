namespace SyncDataModels
{
    public static class GlobalConfig
    {
        public static string TargetDomain = "https://localhost:44302/";
        
        public static string TargetSubDomain = "";
        
        public static string ClientId = "client";
        
        public static string ClientSecret = "def2edf7-5d42-4edc-a84a-30136c340e13";
        
        public const string ClientScope = "default-api";
        
        public static ClientInfo ClientInfo { get; set; }
        
        public static Tenancy CurrentTenant { get; set; }
    }
    
    public class GlobalConst
    {
        public const string AvailableDomitoryDataUrl = "api/services/app/dormitory/getAvailableData";
        
        public const string SendDomitoryDataInfoUrl = "api/services/app/dormitory/sendData";
    }
}