﻿using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Zero.Authorization;
using Zero.Web.Common;

namespace Zero.Web.Areas.App.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] =
                new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Dashboard,
                        L("Dashboard"),
                        icon: "la la-dashboard",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Dashboard)
                    )
                )
                .AddItem(new MenuItemDefinition(
                            AppPageNames.Cms.Settings,
                            L("Settings"),
                            icon: "la la-cog"
                        )

                        #region Settings Cms

                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.ImageBlockGroup,
                                L("ImageBlockGroups"),
                                url: $"{ZeroConst.CmsAreas}/ImageBlockGroup",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.ImageBlockGroup)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.ImageBlock,
                                L("ImageBlocks"),
                                url: $"{ZeroConst.CmsAreas}/ImageBlock",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.ImageBlock)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.PageTheme,
                                L("PageThemes"),
                                url: $"{ZeroConst.CmsAreas}/PageTheme",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.PageTheme)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.PageLayout,
                                L("PageLayouts"),
                                url: $"{ZeroConst.CmsAreas}/PageLayout",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.PageLayout)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Widget,
                                L("Widgets"),
                                url: $"{ZeroConst.CmsAreas}/Widget",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Widget)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Page,
                                L("Pages"),
                                url: $"{ZeroConst.CmsAreas}/Page",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Page)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Tags,
                                L("Tags"),
                                url: $"{ZeroConst.CmsAreas}/Tags",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Tags)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.MenuGroup,
                                L("MenuGroups"),
                                url: $"{ZeroConst.CmsAreas}/MenuGroup",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.MenuGroup)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Menu,
                                L("Menus"),
                                url: $"{ZeroConst.CmsAreas}/Menu",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Menu)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Category,
                                L("Categories"),
                                url: $"{ZeroConst.CmsAreas}/Category",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Category)
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppPageNames.Cms.Post,
                                L("Posts"),
                                url: $"{ZeroConst.CmsAreas}/Post",
                                permissionDependency: new SimplePermissionDependency(CmsPermissions.Post)
                            )
                        )

                    #endregion

                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.CardMenu,
                        L("CardMenu"),
                        icon: "la la-credit-card"
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Park.CardType,
                            L("CardType"),
                            url: $"{ZeroConst.ParkAreas}/CardType",
                            permissionDependency: new SimplePermissionDependency(ParkPermissions.CardType)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Park.Card,
                            L("Card"),
                            url: $"{ZeroConst.ParkAreas}/Card",
                            permissionDependency: new SimplePermissionDependency(ParkPermissions.Card)
                        )
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.ConfigurePark,
                        L("ConfigurePark"),
                        url: $"{ZeroConst.ParkAreas}/ConfigurePark",
                        permissionDependency: new SimplePermissionDependency(ParkPermissions.ConfigurePark)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.History,
                        L("History"),
                        url: $"{ZeroConst.ParkAreas}/History",
                        permissionDependency: new SimplePermissionDependency(ParkPermissions.History)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.VehicleType,
                        L("VehicleType"),
                        url: $"{ZeroConst.ParkAreas}/VehicleType",
                        permissionDependency: new SimplePermissionDependency(ParkPermissions.VehicleType)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.Fare,
                        L("Fare"),
                        url: $"{ZeroConst.ParkAreas}/Fare",
                        permissionDependency: new SimplePermissionDependency(ParkPermissions.Fare)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Park.Resident,
                        L("Resident"),
                        url: $"{ZeroConst.ParkAreas}/Resident",
                        permissionDependency: new SimplePermissionDependency(ParkPermissions.Resident)
                    )
                )

                #region Administration

                .AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.DashboardWidget,
                            L("DashboardWidgets"),
                            url: "App/DashboardWidget",
                            icon: "la la-database",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.DashboardWidget)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Common.EmailTemplate,
                            L("EmailTemplates"),
                            url: "App/EmailTemplate",
                            icon: "flaticon-email",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_EmailTemplates)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.CurrencyRate,
                            L("CurrencyRates"),
                            url: "App/CurrencyRate",
                            icon: "la la-money",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.CurrencyRate)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Tenants,
                            L("Tenants"),
                            url: "App/Tenants",
                            icon: "flaticon-list-3",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Editions,
                            L("Editions"),
                            url: "App/Editions",
                            icon: "flaticon-app",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "App/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Roles,
                            L("Roles"),
                            url: "App/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Users,
                            L("Users"),
                            url: "App/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Users)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Languages,
                            L("Languages"),
                            url: "App/Languages",
                            icon: "flaticon-tabs",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Languages)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "App/AuditLogs",
                            icon: "flaticon-folder-1",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_AuditLogs)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "App/Maintenance",
                            icon: "flaticon-lock",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Maintenance)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Common.UiCustomization,
                            L("VisualSettings"),
                            url: "App/UiCustomization",
                            icon: "flaticon-medical",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_UiCustomization)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.WebhookSubscriptions,
                            L("WebhookSubscriptions"),
                            url: "App/WebhookSubscription",
                            icon: "flaticon2-world",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_WebhookSubscription)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Common.DynamicProperties,
                            L("DynamicProperties"),
                            url: "App/DynamicProperty",
                            icon: "flaticon-interface-8",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_DynamicProperties)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Settings,
                            L("SystemSettings"),
                            url: "App/HostSettings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Tenant.Settings,
                            L("TenantSettings"),
                            url: "App/Settings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Tenant_Settings)
                        )
                    )
                );

            #endregion

            if (ZeroConst.MultiTenancyEnabled)
                menu.AddItem(new MenuItemDefinition(
                        AppPageNames.Tenant.SubscriptionManagement,
                        L("Subscription"),
                        url: "App/SubscriptionManagement",
                        icon: "flaticon-refresh",
                        permissionDependency: new SimplePermissionDependency(AppPermissions
                            .Pages_Administration_Tenant_SubscriptionManagement)
                    )
                );

            if (WebConsts.HangfireDashboardEnabled)
                menu.AddItem(new MenuItemDefinition(
                        AppPageNames.Host.HangfireDashboard,
                        L("HangfireDashboard"),
                        url: WebConsts.HangfireDashboardEndPoint,
                        icon: "flaticon-refresh",
                        permissionDependency: new SimplePermissionDependency(AppPermissions
                            .Pages_Administration_HangfireDashboard)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ZeroConst.LocalizationSourceName);
        }
    }
}