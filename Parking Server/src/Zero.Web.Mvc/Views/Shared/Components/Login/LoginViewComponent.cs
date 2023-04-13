using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Microsoft.AspNetCore.Mvc;
using Zero.Configuration;
using Zero.Web.Models.Account;

namespace Zero.Web.Views.Shared.Components.Login
{
    public class LoginViewComponent : ZeroViewComponent
    {
        #region Constructor

        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ISettingManager _settingManager;

        public LoginViewComponent(
            ISettingManager settingManager,
            IMultiTenancyConfig multiTenancyConfig
        )
        {
            _settingManager = settingManager;
            _multiTenancyConfig = multiTenancyConfig;
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
            ViewBag.ReturnUrl = "/trang-chu";
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
    }
}