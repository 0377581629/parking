using System;
using System.Threading;
using System.Threading.Tasks;
using SyncDataModels;

namespace SyncDataClient
{
    public static class Init
    {
        /// <summary>
        /// Khởi tạo thông tin kết nối
        /// </summary>
        /// <param name="domain">Tên miền muốn kết nối/ Mặc định https://ib-pms.com</param>
        /// <param name="subDomain">Đơn vị muốn kết nối/ Mặc định rỗng</param>
        /// <param name="userName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>True nếu kết nối thành công</returns>
        public static void SetInfo(string domain = "", string subDomain = "", string userName = "", string password = "")
        {
            if (!string.IsNullOrEmpty(domain))
                GlobalConfig.TargetDomain = domain;
            if (!string.IsNullOrEmpty(subDomain))
                GlobalConfig.TargetSubDomain = subDomain;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new Exception("Missing Username and Password");
            GlobalConfig.ClientInfo = new ClientInfo()
            {
                UserName = userName,
                Password = password
            };
        }

        public static void SetKey(string clientId = "", string clientSecret = "")
        {
            if (!string.IsNullOrEmpty(clientId))
                GlobalConfig.ClientId = clientId;
            if (!string.IsNullOrEmpty(clientSecret))
                GlobalConfig.ClientSecret = clientSecret;
        }
    }
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return MyTaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            MyTaskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}