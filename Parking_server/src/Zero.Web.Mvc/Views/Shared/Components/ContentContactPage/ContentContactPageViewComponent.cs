using System.Threading.Tasks;
using DPS.Park.Application.Shared.Interface.ConfigurePark;
using Microsoft.AspNetCore.Mvc;
using Zero.Web.Models.FrontPages.Contact;

namespace Zero.Web.Views.Shared.Components.ContentContactPage
{
    public class ContentContactPageViewComponent: ZeroViewComponent
    {
        private readonly IConfigureParkAppService _configureParkAppService;

        public ContentContactPageViewComponent(IConfigureParkAppService configureParkAppService)
        {
            _configureParkAppService = configureParkAppService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ContactViewModel()
            {
                ConfigurePark = await _configureParkAppService.Get()
            };
            return View(model);
        }
    }
}