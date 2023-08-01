namespace SyncDataClient
{
    public static class GlobalConfig
    {
        #region Common

        public const string TargetDomain = "https://localhost:44302/";
        public const string TenantId = null;
        public const string ClientId = "client";
        public const string ClientSecret = "def2edf7-5d42-4edc-a84a-30136c340e13";
        public const string ClientScope = "default-api";
        public const string UserName = "admin";
        public const string Password = "123qwe";

        #endregion

        #region Upload History Image

        public const string UploadImageToServerUrl = "App/FilesManager/Upload";
        public const string HistoryImageFolderName = "Host/HistoryImage";

        #endregion

        #region License plate detection and recognization

        public const string PythonUploadImageUrl = "http://gleximpark.azurewebsites.net/upload";

        #endregion
    }
}