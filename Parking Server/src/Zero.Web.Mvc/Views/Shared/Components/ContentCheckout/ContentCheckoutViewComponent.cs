using System.Threading.Tasks;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Interface.Common;
using Microsoft.AspNetCore.Mvc;
using Zero.Web.Models.FrontPages.Checkout;

namespace Zero.Web.Views.Shared.Components.ContentCheckout
{
    public class ContentCheckoutViewComponent : ZeroViewComponent
    {
        private readonly IParkPublicAppService _parkPublicAppService;

        public ContentCheckoutViewComponent(IParkPublicAppService parkPublicAppService)
        {
            _parkPublicAppService = parkPublicAppService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var student = await _parkPublicAppService.GetStudentByUserId(new ParkPublicInput()
            {
                UserId = AbpSession.UserId
            });

            var model = new CheckoutViewModel()
            {
                Order = new CreateOrEditOrderDto(),
                StudentCode = student.Code,
                StudentName = student.Name
            };
            return View(model);
        }
    }
}