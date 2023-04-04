namespace SyncDataModels
{
    public static class GlobalConfig
    {
        public const string TargetDomain = "https://localhost:44302";
        
        public const string TenantId = "2";
        
        public const string ClientId = "client";
        
        public const string ClientSecret = "def2edf7-5d42-4edc-a84a-30136c340e13";
        
        public const string ClientScope = "default-api";

        public const string UserName = "admin";

        public const string Password = "123qwe";
    }
    
    public class GlobalConst
    {
        public const string AvailableDomitoryDataUrl = "api/services/app/ParkSync/GetStudentActiveInfo";
        
        public const string SendDomitoryDataInfoUrl = "api/services/app/dormitory/sendData";
    }
}