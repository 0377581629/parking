using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Zero.Web.Views.Shared.Components.ContentAbout
{
    public class ContentAboutViewComponent: ZeroViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}