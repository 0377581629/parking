using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Interface.Order;
using DPS.Park.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Web.Areas.Park.Models.Order;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Order)]
    public class OrderController : ZeroControllerBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        public ActionResult Index()
        {
            var viewModel = new OrderViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Order_Create, ParkPermissions.Order_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetOrderForEditOutput getOrderForEditOutput;

            if (id.HasValue)
            {
                getOrderForEditOutput = await _orderAppService.GetOrderForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getOrderForEditOutput = new GetOrderForEditOutput
                {
                    Order = new CreateOrEditOrderDto()
                    {
                        Code = StringHelper.ShortIdentity()
                    }
                };
            }

            var viewModel = new CreateOrEditOrderViewModel
            {
                Order = getOrderForEditOutput.Order,
                ListOrderStatus = ParkHelper.ListOrderStatus(0, LocalizationSource)
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}