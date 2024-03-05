using System.Threading.Tasks;
using DPS.Cms.Application.Shared.Dto.Common;
using DPS.Cms.Application.Shared.Dto.Page;
using DPS.Cms.Application.Shared.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Zero.Web.Models.FrontPages;
using Zero.Web.Session;

namespace Zero.Web.Views.Shared.Components.ContentPostDetail
{
    public class ContentPostDetailViewComponent: ZeroViewComponent
    {
        private readonly ICmsPublicAppService _cmsPublicAppService;

        public ContentPostDetailViewComponent(ICmsPublicAppService cmsPublicAppService)
        {
            _cmsPublicAppService = cmsPublicAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new PageWidgetViewModel
            {
                PageWidget = new PageWidgetDto(),
            };

            var post = await _cmsPublicAppService.GetPost(new CmsPublicInput()
            {
                PostSlugWithCode = ViewBag.PostSlugWithCode
            });

            viewModel.PageWidget.Post = post;
            return View(viewModel);
        }
    }
}