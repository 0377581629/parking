using System.Linq;
using System.Threading.Tasks;
using DPS.Cms.Application.Shared.Dto.Common;
using DPS.Cms.Application.Shared.Dto.Page;
using DPS.Cms.Application.Shared.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Zero.Web.Models.FrontPages;

namespace Zero.Web.Views.Shared.Components.ContentPostList
{
    public class ContentPostListViewComponent: ZeroViewComponent
    {
        private readonly ICmsPublicAppService _cmsPublicAppService;

        public ContentPostListViewComponent(ICmsPublicAppService cmsPublicAppService)
        {
            _cmsPublicAppService = cmsPublicAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new PageWidgetViewModel
            {
                PageWidget = new PageWidgetDto(),
            };

            var erFilterInp = new CmsPublicInput
            {
                Sorting = "creationTime desc",
                MaxResultCount = 5
            };

            if (ViewBag.PostViewingPage != null)
            {
                switch (ViewBag.PostViewingPage)
                {
                    case string when int.TryParse(ViewBag.PostViewingPage, out int viewPage):
                    {
                        ViewBag.PostViewingPage = viewPage;
                        if (viewPage < 1)
                            ViewBag.PostViewingPage = 1;
                        erFilterInp.SkipCount = (ViewBag.PostViewingPage - 1) * erFilterInp.MaxResultCount;
                        break;
                    }
                    case int:
                    {
                        if (ViewBag.PostViewingPage < 1)
                            ViewBag.PostViewingPage = 1;
                        erFilterInp.SkipCount = (ViewBag.PostViewingPage - 1) * erFilterInp.MaxResultCount;
                        break;
                    }
                }
            }

            var pagedPosts = await _cmsPublicAppService.GetPagedPosts(erFilterInp);

            viewModel.PageWidget.ListPosts = pagedPosts.Items.ToList();
            viewModel.PageWidget.PostsCount = pagedPosts.TotalCount;
            
            viewModel.PageWidget.PostsFiltering = ViewBag.PostsFiltering;
            viewModel.PageWidget.PostsSorting = erFilterInp.Sorting;
            viewModel.PageWidget.PostsMaxResultCount = erFilterInp.MaxResultCount;
            viewModel.PageWidget.PostsSkipCount = erFilterInp.SkipCount;

            return View(viewModel);
        }
    }
}