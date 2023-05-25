namespace ParkingLib
{
    public static class GlobalConfig
    {
        #region Common
        
        public const string TenantId = null;

        public const string DateTimePickerFormat = "dd/MM/yyyy";

        #endregion

        #region License plate detection and recognization

        public const string PythonUploadImageUrl = "http://localhost:5000/upload";

        #endregion

        #region ParkingServer
        
        public const string ParkingServerHost = "https://localhost:44302/";

        public const string ImageStorageFolder = "Files/Host/HistoryImage/";

        #endregion
    }
}