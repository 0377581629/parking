using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Runtime.Session;
using DPS.Cms.Application.Shared.Dto.Common;
using DPS.Cms.Application.Shared.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Zero.Identity;
using Zero.MultiTenancy;
using Zero.Url;
using Zero.Web.Models.FrontPages;

namespace Zero.Web.Controllers
{
    public class HomeController : ZeroControllerBase
    {
        private readonly SignInManager _signInManager;
        private readonly ICmsPublicAppService _cmsPublicAppService;
        private readonly IWebUrlService _webUrlService;
        private readonly TenantManager _tenantManager;

        public HomeController(SignInManager signInManager, ICmsPublicAppService cmsPublicAppService,
            IWebUrlService webUrlService, TenantManager tenantManager)
        {
            _signInManager = signInManager;
            _cmsPublicAppService = cmsPublicAppService;
            _webUrlService = webUrlService;
            _tenantManager = tenantManager;
        }

        // Use for Back pages - uncomment when the project not have front pages
        // public async Task<IActionResult> Index(string redirect = "", bool forceNewRegistration = false)
        // {
        //     if (forceNewRegistration)
        //     {
        //         await _signInManager.SignOutAsync();
        //     }
        //
        //     if (redirect == "TenantRegistration")
        //     {
        //         return RedirectToAction("SelectEdition", "TenantRegistration");
        //     }
        //
        //     return AbpSession.UserId.HasValue
        //         ? RedirectToAction("Index", "Home", new {area = "App"})
        //         : RedirectToAction("Login", "Account");
        // }
        
        public async Task<ActionResult> Index()
        {
            var page = await _cmsPublicAppService.GetHomePage();
        
            var pageViewModel = new PageViewModel
            {
                Page = page,
                Widgets = await _cmsPublicAppService.GetPageWidgets(new CmsInput
                {
                    PageId = page?.Id,
                    PageSlug = page?.Slug
                }),
                Blocks = await _cmsPublicAppService.GetPageLayoutBlocks(new CmsInput
                {
                    PageLayoutId = page?.PageLayoutId
                })
            };
        
            ViewBag.AdminWebSiteRootAddress = AdminWebsiteRootAddress;
            ViewBag.WebSiteRootAddress = WebsiteRootAddress;
        
            if (page == null) return View(pageViewModel);
        
            ViewBag.Title = !page.TitleDefault ? page.Title : GlobalConfig.AppName;
            ViewBag.MetaTitle = !page.TitleDefault ? page.Title : GlobalConfig.AppName;
            ViewBag.MetaDesciption = !page.DescriptionDefault ? page.Description : GlobalConfig.AppDescription;
            ViewBag.MetaAuthor = !page.AuthorDefault ? page.Author : GlobalConfig.AppAuthor;
        
            return View(pageViewModel);
        }

        public async Task<ActionResult> Pages(string slug)
        {
            var page = await _cmsPublicAppService.GetPageBySlug(new GetPageInput {PageSlug = slug});

            var pageViewModel = new PageViewModel
            {
                Page = page,
                Widgets = await _cmsPublicAppService.GetPageWidgets(new CmsInput
                {
                    PageId = page?.Id,
                    PageSlug = slug
                }),
                Blocks = await _cmsPublicAppService.GetPageLayoutBlocks(new CmsInput
                {
                    PageLayoutId = page?.PageLayoutId
                })
            };

            ViewBag.AdminWebSiteRootAddress = AdminWebsiteRootAddress;
            ViewBag.WebSiteRootAddress = WebsiteRootAddress;

            if (page == null) return View("Index", pageViewModel);

            ViewBag.Title = !page.TitleDefault ? page.Title : GlobalConfig.AppName;
            ViewBag.MetaTitle = !page.TitleDefault ? page.Title : GlobalConfig.AppName;
            ViewBag.MetaDesciption = !page.DescriptionDefault ? page.Description : GlobalConfig.AppDescription;
            ViewBag.MetaAuthor = !page.AuthorDefault ? page.Author : GlobalConfig.AppAuthor;

            return View("Index", pageViewModel);
        }

        #region Helper

        private string AdminWebsiteRootAddress
        {
            get
            {
                var tenancyName = "";
                if (!AbpSession.TenantId.HasValue)
                    return _webUrlService.GetServerRootAddress(tenancyName).EnsureEndsWith('/');
                var tenant = _tenantManager.GetById(AbpSession.GetTenantId());
                tenancyName = tenant.TenancyName;
                return _webUrlService.GetServerRootAddress(tenancyName).EnsureEndsWith('/');
            }
        }

        private string WebsiteRootAddress
        {
            get
            {
                var tenancyName = "";
                if (!AbpSession.TenantId.HasValue)
                    return _webUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/');
                var tenant = _tenantManager.GetById(AbpSession.GetTenantId());
                tenancyName = tenant.TenancyName;

                var x = _webUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/');
                return _webUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/');
            }
        }

        #endregion
    }
}