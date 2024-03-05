using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zero.Web.Models.FrontPages.UserProfile;
using Zero.Web.Session;

namespace Zero.Web.Views.Shared.Components.ContentUserProfile
{
    public class ContentUserProfileViewComponent : ZeroViewComponent
    {
        private readonly IPerRequestSessionCache _perRequestSessionCache;

        public ContentUserProfileViewComponent(IPerRequestSessionCache perRequestSessionCache)
        {
            _perRequestSessionCache = perRequestSessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new UserProfileViewModel();
            if (!AbpSession.UserId.HasValue)
                return View(viewModel);

            viewModel.User = (await _perRequestSessionCache.GetCurrentLoginInformationsAsync()).User;

            return View(viewModel);
        }
    }
}