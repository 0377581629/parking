﻿using System.Globalization;

namespace Zero
{
    public class ZeroConst
    {
        public const string LocalizationSourceName = "AbpZero";

        public const string ConnectionStringName = "Default";

        public static bool MultiTenancyEnabled = false;

        public static bool AllowTenantsToChangeEmailSettings = true;

        public const string Currency = "VND";

        public const string CurrencySign = "đ";

        public const string AbpApiClientUserAgent = "AbpApiClient";

        // Note:
        // Minimum accepted payment amount. If a payment amount is less then that minimum value payment progress will continue without charging payment
        // Even though we can use multiple payment methods, users always can go and use the highest accepted payment amount.
        //For example, you use Stripe and PayPal. Let say that stripe accepts min 5$ and PayPal accepts min 3$. If your payment amount is 4$.
        // User will prefer to use a payment method with the highest accept value which is a Stripe in this case.
        public const decimal MinimumUpgradePaymentAmount = 1M;

        #region Extent

        /// <summary>
        /// Default min string required field length
        /// </summary>
        public const int MinStrLength = 1;

        /// <summary>
        /// Default max string required field length
        /// </summary>
        public const int MaxStrLength = 512;

        /// <summary>
        /// Default min name required field length
        /// </summary>
        public const int MinNameLength = 1;

        /// <summary>
        /// Default max name required field length
        /// </summary>
        public const int MaxNameLength = 512;

        /// <summary>
        /// Default min string required field length
        /// </summary>
        public const int MinCodeLength = 1;

        /// <summary>
        /// Use for nested object
        /// </summary>
        public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;

        /// <summary>
        /// Use for nested object
        /// </summary>
        public const int CodeUnitLength = 5;

        /// <summary>
        /// Use for nested object
        /// </summary>
        private const int MaxDepth = 16;

        private const string ViewResourcesAreas = "/view-resources/Areas/";

        public const string ScriptPathApp = "/view-resources/Areas/App";

        public static string CmsAreas = "Cms";
        public static string ScriptPathCms = $"{ViewResourcesAreas}{CmsAreas}";
        
        public static string ParkAreas = "Park";
        public static string ScriptPathPark = $"{ViewResourcesAreas}{ParkAreas}";
        
        public static NumberFormatInfo NumberFormatInfo = new() {NumberDecimalSeparator = "."};
        public static string DateFormat = "dd/MM/yyyy HH:mm:ss";
        public static string ReportExtension = ".trdx";
        public const string DefaultAvatarUrl = "../../Common/Images/default-profile-picture.png";
        public const string DefaultImageUrl = "../../Common/Images/default-image.png";
        
        public static string ReportingAreas = "Reporting";
        public static string ScriptPathReporting = $"{ViewResourcesAreas}{ReportingAreas}";
        public static string LeftReportHeaderConfigKey = "Reporting.Header.Left";
        public static string RightReportHeaderConfigKey = "Reporting.Header.Right";
        
        #endregion
    }
}