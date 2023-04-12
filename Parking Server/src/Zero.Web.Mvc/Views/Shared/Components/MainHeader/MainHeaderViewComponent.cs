using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using DPS.Cms.Application.Shared.Interfaces.Common;
using DPS.Park.Application.Shared.Interface.ConfigurePark;
using Microsoft.AspNetCore.Mvc;
using Zero.MultiTenancy;
using Zero.Url;
using Zero.Web.Session;

namespace Zero.Web.Views.Shared.Components.MainHeader
{
    public class MainHeaderViewComponent : ZeroViewComponent
    {
        #region Constructor

        private readonly ICmsPublicAppService _cmsPublicAppService;
        private readonly IConfigureParkAppService _configureParkAppService;

        private readonly ILanguageManager _languageManager;
        private readonly IAbpSession _abpSession;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IWebUrlService _webUrlService;
        private readonly TenantManager _tenantManager;

        public MainHeaderViewComponent(
            ICmsPublicAppService cmsPublicAppService,
            ILanguageManager languageManager, IAbpSession abpSession, IPerRequestSessionCache sessionCache,
            TenantManager tenantManager,
            IWebUrlService webUrlService,
            IConfigureParkAppService configureParkAppService)
        {
            _cmsPublicAppService = cmsPublicAppService;
            _languageManager = languageManager;
            _abpSession = abpSession;
            _sessionCache = sessionCache;
            _tenantManager = tenantManager;
            _webUrlService = webUrlService;
            _configureParkAppService = configureParkAppService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tenancyName = "";
            if (_abpSession.TenantId.HasValue)
            {
                var tenant = await _tenantManager.GetByIdAsync(_abpSession.GetTenantId());
                tenancyName = tenant.TenancyName;
            }

            var viewModel = new HeaderViewModel
            {
                LoginInformation = await _sessionCache.GetCurrentLoginInformationsAsync(),
                Languages = _languageManager.GetActiveLanguages().ToList(),
                CurrentLanguage = _languageManager.CurrentLanguage,
                AdminWebSiteRootAddress = _webUrlService.GetServerRootAddress(tenancyName).EnsureEndsWith('/'),
                WebSiteRootAddress = _webUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/'),

                ConfigurePark = await _configureParkAppService.Get(),
                Menus = await _cmsPublicAppService.GetDefaultMenus()
            };

            return View(viewModel);
        }
    }
}