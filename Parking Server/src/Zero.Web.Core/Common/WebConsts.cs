using System.Collections.Generic;

namespace Zero.Web.Common
{
    public static class WebConsts
    {
        public const string SwaggerUiEndPoint = "/swagger";
        public const string HangfireDashboardEndPoint = "/hangfire";

        public static bool SwaggerUiEnabled = true;
        public static bool HangfireDashboardEnabled = true;

        public static List<string> ReCaptchaIgnoreWhiteList = new List<string>
        {
            ZeroConst.AbpApiClientUserAgent
        };
    }
}
