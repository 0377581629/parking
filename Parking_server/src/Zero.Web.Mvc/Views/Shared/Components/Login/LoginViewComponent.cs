using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Zero.Configuration;
using Zero.MultiTenancy;
using Zero.Url;
using Zero.Web.Models.Account;

namespace Zero.Web.Views.Shared.Components.Login
{
    public class LoginViewComponent : ZeroViewComponent
    {
        #region Constructor

        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ISettingManager _settingManager;
        private readonly TenantManager _tenantManager;
        private readonly IWebUrlService _webUrlService;

        public LoginViewComponent(
            ISettingManager settingManager,
            IMultiTenancyConfig multiTenancyConfig, TenantManager tenantManager, IWebUrlService webUrlService)
        {
            _settingManager = settingManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _webUrlService = webUrlService;
        }

        #endregion

        #region Private Methods

        private bool UseCaptchaOnLogin()
        {
            return _settingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnLogin);
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int pageWidgetId)
        {
            var tenancyName = await GetCurrentTenancyName();
            var websiteAddress = _webUrlService.GetSiteRootAddress(tenancyName);

            ViewBag.ReturnUrl = websiteAddress;
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            ViewBag.SingleSignIn = "";
            ViewBag.UseCaptcha = UseCaptchaOnLogin();
            if (TempData["LoginErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["LoginErrorMessage"];
            return View(
                new LoginFormViewModel
                {
                    SuccessMessage = "",
                    UserNameOrEmailAddress = ""
                });
        }

        private async Task<string> GetCurrentTenancyName()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return "";
            }

            var tenant = await _tenantManager.GetByIdAsync(AbpSession.GetTenantId());
            return tenant.TenancyName;
        }
    }
}