﻿using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Json;
using Abp.Net.Mail;
using Abp.Zero.Configuration;
using Microsoft.Extensions.Configuration;
using Zero.Authentication;
using Zero.DashboardCustomization;
using Newtonsoft.Json;
using Zero.Abp.Payments;
using Zero.MultiTenancy.Payments;

namespace Zero.Configuration
{
    /// <summary>
    /// Defines settings for the application.
    /// See <see cref="AppSettings"/> for setting names.
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IPaymentGatewayStore _paymentGatewayStore;

        public AppSettingProvider(IAppConfigurationAccessor configurationAccessor,
            IPaymentGatewayStore paymentGatewayStore)
        {
            _paymentGatewayStore = paymentGatewayStore;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            // Disable TwoFactorLogin by default (can be enabled by UI)
            context.Manager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled)
                .DefaultValue = false.ToString().ToLowerInvariant();

            // Change scope of Email settings
            ChangeEmailSettingScopes(context);

            return GetHostSettings()
                .Union(GetTenantSettings())
                .Union(GetSharedSettings())
                // theme settings
                .Union(GetDefaultThemeSettings())
                .Union(GetTheme2Settings())
                .Union(GetTheme3Settings())
                .Union(GetTheme4Settings())
                .Union(GetTheme5Settings())
                .Union(GetTheme6Settings())
                .Union(GetTheme7Settings())
                .Union(GetTheme8Settings())
                .Union(GetTheme9Settings())
                .Union(GetTheme10Settings())
                .Union(GetTheme11Settings())
                .Union(GetTheme12Settings())
                .Union(GetDashboardSettings())
                .Union(GetExternalLoginProviderSettings());
        }

        private void ChangeEmailSettingScopes(SettingDefinitionProviderContext context)
        {
            if (!ZeroConst.AllowTenantsToChangeEmailSettings)
            {
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Host).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Port).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UserName).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Password).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Domain).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.EnableSsl).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromAddress).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromDisplayName).Scopes =
                    SettingScopes.Application;
            }
        }

        private IEnumerable<SettingDefinition> GetHostSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettings.TenantManagement.AllowSelfRegistration,
                    GetFromAppSettings(AppSettings.TenantManagement.AllowSelfRegistration, "true"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault,
                    GetFromAppSettings(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault, "false")),
                new SettingDefinition(AppSettings.TenantManagement.UseCaptchaOnRegistration,
                    GetFromAppSettings(AppSettings.TenantManagement.UseCaptchaOnRegistration, "true"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.DefaultEdition,
                    GetFromAppSettings(AppSettings.TenantManagement.DefaultEdition, "")),
                new SettingDefinition(AppSettings.UserManagement.SmsVerificationEnabled,
                    GetFromAppSettings(AppSettings.UserManagement.SmsVerificationEnabled, "false"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount,
                    GetFromAppSettings(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount, "7"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettings.HostManagement.BillingLegalName,
                    GetFromAppSettings(AppSettings.HostManagement.BillingLegalName, "")),
                new SettingDefinition(AppSettings.HostManagement.BillingAddress,
                    GetFromAppSettings(AppSettings.HostManagement.BillingAddress, "")),
                new SettingDefinition(AppSettings.Recaptcha.SiteKey, GetFromSettings("Recaptcha:SiteKey"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettings.UiManagement.Theme,
                    GetFromAppSettings(AppSettings.UiManagement.Theme, "default"), isVisibleToClients: true,
                    scopes: SettingScopes.All),
            };
        }

        private IEnumerable<SettingDefinition> GetTenantSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettings.TenantManagement.BillingLegalName,
                    GetFromAppSettings(AppSettings.TenantManagement.BillingLegalName, ""),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettings.TenantManagement.BillingAddress,
                    GetFromAppSettings(AppSettings.TenantManagement.BillingAddress, ""), scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettings.TenantManagement.BillingTaxVatNo,
                    GetFromAppSettings(AppSettings.TenantManagement.BillingTaxVatNo, ""), scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettings.Email.UseHostDefaultEmailSettings,
                    GetFromAppSettings(AppSettings.Email.UseHostDefaultEmailSettings,
                        ZeroConst.MultiTenancyEnabled ? "true" : "false"), scopes: SettingScopes.Tenant)
            };
        }

        private IEnumerable<SettingDefinition> GetSharedSettings()
        {
            var res = new List<SettingDefinition>
            {
                new(AppSettings.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled,
                    GetFromAppSettings(AppSettings.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.UserManagement.IsCookieConsentEnabled,
                    GetFromAppSettings(AppSettings.UserManagement.IsCookieConsentEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.UserManagement.IsQuickThemeSelectEnabled,
                    GetFromAppSettings(AppSettings.UserManagement.IsQuickThemeSelectEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.UserManagement.UseCaptchaOnLogin,
                    GetFromAppSettings(AppSettings.UserManagement.UseCaptchaOnLogin, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.UserManagement.SessionTimeOut.IsEnabled,
                    GetFromAppSettings(AppSettings.UserManagement.SessionTimeOut.IsEnabled, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SessionTimeOut.TimeOutSecond,
                    GetFromAppSettings(AppSettings.UserManagement.SessionTimeOut.TimeOutSecond, "30"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond,
                    GetFromAppSettings(AppSettings.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond, "30"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut,
                    GetFromAppSettings(AppSettings.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.AllowOneConcurrentLoginPerUser,
                    GetFromAppSettings(AppSettings.UserManagement.AllowOneConcurrentLoginPerUser, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.AllowUsingGravatarProfilePicture,
                    GetFromAppSettings(AppSettings.UserManagement.AllowUsingGravatarProfilePicture, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.UseGravatarProfilePicture,
                    GetFromAppSettings(AppSettings.UserManagement.UseGravatarProfilePicture, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.User),
                new SettingDefinition(ZeroConst.LeftReportHeaderConfigKey,
                    GetFromAppSettings(ZeroConst.LeftReportHeaderConfigKey, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(ZeroConst.RightReportHeaderConfigKey,
                    GetFromAppSettings(ZeroConst.RightReportHeaderConfigKey, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),

                // User Subscription 
                new(AppSettings.UserManagement.SubscriptionUser,
                    GetFromAppSettings(AppSettings.UserManagement.SubscriptionUser, "false"),
                    isVisibleToClients: false, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SubscriptionCurrency,
                    GetFromAppSettings(AppSettings.UserManagement.SubscriptionCurrency, "VND"),
                    isVisibleToClients: false, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SubscriptionTrialDays,
                    GetFromAppSettings(AppSettings.UserManagement.SubscriptionTrialDays, "7"),
                    isVisibleToClients: false, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SubscriptionMonthlyPrice,
                    GetFromAppSettings(AppSettings.UserManagement.SubscriptionMonthlyPrice, "30000"),
                    isVisibleToClients: false, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.SubscriptionYearlyPrice,
                    GetFromAppSettings(AppSettings.UserManagement.SubscriptionYearlyPrice, "50000"),
                    isVisibleToClients: false, scopes: SettingScopes.Application | SettingScopes.Tenant),

                // User Self Registration
                new(AppSettings.UserManagement.AllowSelfRegistration,
                    GetFromAppSettings(AppSettings.UserManagement.AllowSelfRegistration, "true"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault,
                    GetFromAppSettings(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant),
                new(AppSettings.UserManagement.UseCaptchaOnRegistration,
                    GetFromAppSettings(AppSettings.UserManagement.UseCaptchaOnRegistration, "true"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),

                // Park Settings
                new(AppSettings.ParkSettings.PhoneToSendMessage,
                    GetFromAppSettings(AppSettings.ParkSettings.PhoneToSendMessage, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.BalanceToSendEmail,
                    GetFromAppSettings(AppSettings.ParkSettings.BalanceToSendEmail, "0"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.Hotline,
                    GetFromAppSettings(AppSettings.ParkSettings.Hotline, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.Name,
                    GetFromAppSettings(AppSettings.ParkSettings.Name, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.Address,
                    GetFromAppSettings(AppSettings.ParkSettings.Address, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.SubAddress1,
                    GetFromAppSettings(AppSettings.ParkSettings.SubAddress1, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.SubAddress2,
                    GetFromAppSettings(AppSettings.ParkSettings.SubAddress2, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new(AppSettings.ParkSettings.Email,
                    GetFromAppSettings(AppSettings.ParkSettings.Email, ""),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
            };

            res.Add(new SettingDefinition(AppSettings.PaymentManagement.AllowTenantUseCustomConfig,
                GetFromAppSettings(AppSettings.PaymentManagement.AllowTenantUseCustomConfig, "false"),
                scopes: SettingScopes.Application, isVisibleToClients: false));

            res.Add(new SettingDefinition(AppSettings.PaymentManagement.UseCustomPaymentConfig,
                GetFromAppSettings(AppSettings.PaymentManagement.UseCustomPaymentConfig, "false"),
                scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

            var activePaymentGateWays = _paymentGatewayStore.GetActiveGateways();
            if (activePaymentGateWays != null && activePaymentGateWays.Any())
            {
                if (activePaymentGateWays.Any(o => o.GatewayType == SubscriptionPaymentGatewayType.Paypal))
                {
                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalIsActive,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalIsActive, "false"),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalEnvironment,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalEnvironment, "sandbox"),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalClientId,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalClientId, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalClientSecret,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalClientSecret, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalDemoUsername,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalDemoUsername, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.PayPalDemoPassword,
                        GetFromAppSettings(AppSettings.PaymentManagement.PayPalDemoPassword, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));
                }

                if (activePaymentGateWays.Any(o => o.GatewayType == SubscriptionPaymentGatewayType.AlePay))
                {
                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.AlePayIsActive,
                        GetFromAppSettings(AppSettings.PaymentManagement.AlePayIsActive, "false"),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.AlePayBaseUrl,
                        GetFromAppSettings(AppSettings.PaymentManagement.AlePayBaseUrl, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.AlePayTokenKey,
                        GetFromAppSettings(AppSettings.PaymentManagement.AlePayTokenKey, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));

                    res.Add(new SettingDefinition(AppSettings.PaymentManagement.AlePayChecksumKey,
                        GetFromAppSettings(AppSettings.PaymentManagement.AlePayChecksumKey, ""),
                        scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: false));
                }
            }

            return res;
        }

        private string GetFromAppSettings(string name, string defaultValue = null)
        {
            return GetFromSettings("App:" + name, defaultValue);
        }

        private string GetFromSettings(string name, string defaultValue = null)
        {
            return _appConfiguration[name] ?? defaultValue;
        }

        private IEnumerable<SettingDefinition> GetDefaultThemeSettings()
        {
            var themeName = "default";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.Skin,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.Skin, "light"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.AsideSkin,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.AsideSkin, "light"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.AllowAsideMinimizing,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.AllowAsideMinimizing,
                        "true"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.DefaultMinimizedAside,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.DefaultMinimizedAside,
                        "false"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.SubmenuToggle,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.SubmenuToggle, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.HoverableAside,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.HoverableAside,
                        "false"), isVisibleToClients: true, scopes: SettingScopes.All),


                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme2Settings()
        {
            var themeName = "theme2";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fixed"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MinimizeType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MinimizeType, "topbar"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme3Settings()
        {
            var themeName = "theme3";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme4Settings()
        {
            var themeName = "theme4";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fixed"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MinimizeType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MinimizeType, "menu"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme5Settings()
        {
            var themeName = "theme5";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fixed"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MinimizeType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MinimizeType, "menu"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme6Settings()
        {
            var themeName = "theme6";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme7Settings()
        {
            var themeName = "theme7";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme8Settings()
        {
            var themeName = "theme8";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fluid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme9Settings()
        {
            var themeName = "theme9";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fixed"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme10Settings()
        {
            var themeName = "theme10";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fluid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme11Settings()
        {
            var themeName = "theme11";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LayoutType,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LayoutType, "fluid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetTheme12Settings()
        {
            var themeName = "theme12";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.SubmenuToggle,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.LeftAside.SubmenuToggle, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SearchActive,
                    GetFromAppSettings(themeName + "." + AppSettings.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }

        private IEnumerable<SettingDefinition> GetDashboardSettings()
        {
            var mvcDefaultSettings = GetDefaultMvcDashboardViews();
            var mvcDefaultSettingsJson = JsonConvert.SerializeObject(mvcDefaultSettings);

            var angularDefaultSettings = GetDefaultAngularDashboardViews();
            var angularDefaultSettingsJson = JsonConvert.SerializeObject(angularDefaultSettings);

            return new[]
            {
                new SettingDefinition(
                    AppSettings.DashboardCustomization.Configuration + "." +
                    ZeroDashboardCustomizationConsts.Applications.Mvc, mvcDefaultSettingsJson,
                    scopes: SettingScopes.All, isVisibleToClients: true),
                new SettingDefinition(
                    AppSettings.DashboardCustomization.Configuration + "." +
                    ZeroDashboardCustomizationConsts.Applications.Angular, angularDefaultSettingsJson,
                    scopes: SettingScopes.All, isVisibleToClients: true)
            };
        }

        public List<Dashboard> GetDefaultMvcDashboardViews()
        {
            //It is the default dashboard view which your user will see if they don't do any customization.
            return new List<Dashboard>
            {
                new()
                {
                    DashboardName = ZeroDashboardCustomizationConsts.DashboardNames.DefaultTenantDashboard,
                    Pages = new List<Page>
                    {
                        new()
                        {
                            Name = ZeroDashboardCustomizationConsts.DefaultPageName,
                            Widgets = new List<Widget>
                            {
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .GeneralStats, // General Stats
                                    Height = 9,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 19
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .ProfitShare, // Profit Share
                                    Height = 13,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 28
                                },
                                new()
                                {
                                    WidgetId =
                                        ZeroDashboardCustomizationConsts.Widgets.Tenant
                                            .MemberActivity, // Memeber Activity
                                    Height = 13,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 28
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .RegionalStats, // Regional Stats
                                    Height = 14,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 5
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .DailySales, // Daily Sales
                                    Height = 9,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 19
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .TopStats, // Top Stats
                                    Height = 5,
                                    Width = 12,
                                    PositionX = 0,
                                    PositionY = 0
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .SalesSummary, // Sales Summary
                                    Height = 14,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 5
                                }
                            }
                        }
                    }
                },
                new()
                {
                    DashboardName = ZeroDashboardCustomizationConsts.DashboardNames.DefaultHostDashboard,
                    Pages = new List<Page>
                    {
                        new()
                        {
                            Name = ZeroDashboardCustomizationConsts.DefaultPageName,
                            Widgets = new List<Widget>
                            {
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .TopStats, // Top Stats
                                    Height = 6,
                                    Width = 12,
                                    PositionX = 0,
                                    PositionY = 0
                                },
                                new()
                                {
                                    WidgetId =
                                        ZeroDashboardCustomizationConsts.Widgets.Host
                                            .IncomeStatistics, // Income Statistics
                                    Height = 11,
                                    Width = 7,
                                    PositionX = 0,
                                    PositionY = 6
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .RecentTenants, // Recent tenants
                                    Height = 10,
                                    Width = 5,
                                    PositionX = 7,
                                    PositionY = 17
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .SubscriptionExpiringTenants, // Subscription expiring tenants
                                    Height = 10,
                                    Width = 7,
                                    PositionX = 0,
                                    PositionY = 17
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .EditionStatistics, // Edition statistics
                                    Height = 11,
                                    Width = 5,
                                    PositionX = 7,
                                    PositionY = 6
                                }
                            }
                        }
                    }
                }
            };
        }

        public List<Dashboard> GetDefaultAngularDashboardViews()
        {
            //It is the default dashboard view which your user will see if they don't do any customization.
            return new List<Dashboard>
            {
                new()
                {
                    DashboardName = ZeroDashboardCustomizationConsts.DashboardNames.DefaultTenantDashboard,
                    Pages = new List<Page>
                    {
                        new()
                        {
                            Name = ZeroDashboardCustomizationConsts.DefaultPageName,
                            Widgets = new List<Widget>
                            {
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .TopStats, // Top Stats
                                    Height = 4,
                                    Width = 12,
                                    PositionX = 0,
                                    PositionY = 0
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .SalesSummary, // Sales Summary
                                    Height = 12,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 4
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .RegionalStats, // Regional Stats
                                    Height = 12,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 4
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .GeneralStats, // General Stats
                                    Height = 8,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 16
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .DailySales, // Daily Sales
                                    Height = 8,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 16
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Tenant
                                        .ProfitShare, // Profit Share
                                    Height = 11,
                                    Width = 6,
                                    PositionX = 0,
                                    PositionY = 24
                                },
                                new()
                                {
                                    WidgetId =
                                        ZeroDashboardCustomizationConsts.Widgets.Tenant
                                            .MemberActivity, // Member Activity
                                    Height = 11,
                                    Width = 6,
                                    PositionX = 6,
                                    PositionY = 24
                                }
                            }
                        }
                    }
                },
                new()
                {
                    DashboardName = ZeroDashboardCustomizationConsts.DashboardNames.DefaultHostDashboard,
                    Pages = new List<Page>
                    {
                        new()
                        {
                            Name = ZeroDashboardCustomizationConsts.DefaultPageName,
                            Widgets = new List<Widget>
                            {
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .TopStats, // Top Stats
                                    Height = 4,
                                    Width = 12,
                                    PositionX = 0,
                                    PositionY = 0
                                },
                                new()
                                {
                                    WidgetId =
                                        ZeroDashboardCustomizationConsts.Widgets.Host
                                            .IncomeStatistics, // Income Statistics
                                    Height = 8,
                                    Width = 7,
                                    PositionX = 0,
                                    PositionY = 4
                                },
                                new()
                                {
                                    WidgetId =
                                        ZeroDashboardCustomizationConsts.Widgets.Host
                                            .RecentTenants, // Recent tenants
                                    Height = 9,
                                    Width = 5,
                                    PositionX = 7,
                                    PositionY = 12
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .SubscriptionExpiringTenants, // Subscription expiring tenants
                                    Height = 9,
                                    Width = 7,
                                    PositionX = 0,
                                    PositionY = 12
                                },
                                new()
                                {
                                    WidgetId = ZeroDashboardCustomizationConsts.Widgets.Host
                                        .EditionStatistics, // Edition statistics
                                    Height = 8,
                                    Width = 5,
                                    PositionX = 7,
                                    PositionY = 4
                                }
                            }
                        }
                    }
                }
            };
        }

        private IEnumerable<SettingDefinition> GetExternalLoginProviderSettings()
        {
            return GetFacebookExternalLoginProviderSettings()
                .Union(GetGoogleExternalLoginProviderSettings())
                .Union(GetTwitterExternalLoginProviderSettings())
                .Union(GetMicrosoftExternalLoginProviderSettings())
                .Union(GetOpenIdConnectExternalLoginProviderSettings())
                .Union(GetWsFederationExternalLoginProviderSettings());
        }

        private SettingDefinition[] GetFacebookExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:Facebook:IsEnabled") == true.ToString().ToLowerInvariant();
            var appId = GetFromSettings("Authentication:Facebook:AppId");
            var appSecret = GetFromSettings("Authentication:Facebook:AppSecret");

            var facebookExternalLoginProviderInfo = new FacebookExternalLoginProviderSettings()
            {
                IsEnabled = isEnabled,
                AppId = appId,
                AppSecret = appSecret
            };

            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.Facebook,
                    facebookExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.Facebook_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.Facebook,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                )
            };
        }

        private SettingDefinition[] GetGoogleExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:Google:IsEnabled") == true.ToString().ToLowerInvariant();
            var clientId = GetFromSettings("Authentication:Google:ClientId");
            var clientSecret = GetFromSettings("Authentication:Google:ClientSecret");
            var userInfoEndPoint = GetFromSettings("Authentication:Google:UserInfoEndpoint");

            var googleExternalLoginProviderInfo = new GoogleExternalLoginProviderSettings()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                UserInfoEndpoint = userInfoEndPoint
            };

            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.Google,
                    googleExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.Google_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.Google,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                ),
            };
        }

        private SettingDefinition[] GetTwitterExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:Twitter:IsEnabled") == true.ToString().ToLowerInvariant();
            var consumerKey = GetFromSettings("Authentication:Twitter:ConsumerKey");
            var consumerSecret = GetFromSettings("Authentication:Twitter:ConsumerSecret");

            var twitterExternalLoginProviderInfo = new TwitterExternalLoginProviderSettings
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret
            };

            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.Twitter,
                    twitterExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.Twitter_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.Twitter,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                ),
            };
        }

        private SettingDefinition[] GetMicrosoftExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:Microsoft:IsEnabled") == true.ToString().ToLowerInvariant();
            var consumerKey = GetFromSettings("Authentication:Microsoft:ConsumerKey");
            var consumerSecret = GetFromSettings("Authentication:Microsoft:ConsumerSecret");

            var microsoftExternalLoginProviderInfo = new MicrosoftExternalLoginProviderSettings()
            {
                ClientId = consumerKey,
                ClientSecret = consumerSecret
            };


            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.Microsoft,
                    microsoftExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.Microsoft_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.Microsoft,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                ),
            };
        }

        private SettingDefinition[] GetOpenIdConnectExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:OpenId:IsEnabled") == true.ToString().ToLowerInvariant();
            var clientId = GetFromSettings("Authentication:OpenId:ClientId");
            var clientSecret = GetFromSettings("Authentication:OpenId:ClientSecret");
            var authority = GetFromSettings("Authentication:OpenId:Authority");
            var validateIssuerStr = GetFromSettings("Authentication:OpenId:ValidateIssuer");

            bool.TryParse(validateIssuerStr, out bool validateIssuer);

            var openIdConnectExternalLoginProviderInfo = new OpenIdConnectExternalLoginProviderSettings()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Authority = authority,
                ValidateIssuer = validateIssuer
            };

            var jsonClaimMappings = new List<JsonClaimMapDto>();
            _appConfiguration.GetSection("Authentication:OpenId:ClaimsMapping").Bind(jsonClaimMappings);

            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.OpenIdConnect,
                    openIdConnectExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.OpenIdConnect_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.OpenIdConnect,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.OpenIdConnectMappedClaims,
                    jsonClaimMappings.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application | SettingScopes.Tenant
                )
            };
        }

        private SettingDefinition[] GetWsFederationExternalLoginProviderSettings()
        {
            var isEnabled = GetFromSettings("Authentication:WsFederation:IsEnabled") ==
                            true.ToString().ToLowerInvariant();
            var clientId = GetFromSettings("Authentication:WsFederation:ClientId");
            var wtrealm = GetFromSettings("Authentication:WsFederation:Wtrealm");
            var authority = GetFromSettings("Authentication:WsFederation:Authority");
            var tenant = GetFromSettings("Authentication:WsFederation:Tenant");
            var metaDataAddress = GetFromSettings("Authentication:WsFederation:MetaDataAddress");

            var wsFederationExternalLoginProviderInfo = new WsFederationExternalLoginProviderSettings()
            {
                ClientId = clientId,
                Tenant = tenant,
                Authority = authority,
                Wtrealm = wtrealm,
                MetaDataAddress = metaDataAddress
            };

            var jsonClaimMappings = new List<JsonClaimMapDto>();
            _appConfiguration.GetSection("Authentication:WsFederation:ClaimsMapping").Bind(jsonClaimMappings);

            return new[]
            {
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Host.WsFederation,
                    wsFederationExternalLoginProviderInfo.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application,
                    isEncrypted: true
                ),
                new SettingDefinition( //default is empty for tenants
                    AppSettings.ExternalLoginProvider.Tenant.WsFederation,
                    "",
                    isVisibleToClients: false,
                    scopes: SettingScopes.Tenant,
                    isEncrypted: true
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.Tenant.WsFederation_IsDeactivated,
                    (!isEnabled).ToString().ToLowerInvariant(),
                    isVisibleToClients: true,
                    scopes: SettingScopes.Tenant
                ),
                new SettingDefinition(
                    AppSettings.ExternalLoginProvider.WsFederationMappedClaims,
                    jsonClaimMappings.ToJsonString(),
                    isVisibleToClients: false,
                    scopes: SettingScopes.Application | SettingScopes.Tenant
                )
            };
        }
    }
}