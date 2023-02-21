using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Interface.Card;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Web.Areas.Park.Models.Card;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Card)]
    public class CardController: ZeroControllerBase
    {
        private readonly ICardAppService _cardAppService;

        public CardController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }
        
        public ActionResult Index()
        {
            var viewModel = new CardViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }
        
        [AbpMvcAuthorize(ParkPermissions.Card_Create, ParkPermissions.Card_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCardForEditOutput getCardForEditOutput;

            if (id.HasValue)
            {
                getCardForEditOutput = await _cardAppService.GetCardForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getCardForEditOutput = new GetCardForEditOutput
                {
                    Card = new CreateOrEditCardDto()
                    {
                        Code = StringHelper.ShortIdentity(),
                        IsActive = true
                    }
                };
            }

            var viewModel = new CreateOrEditCardViewModel()
            {
                Card = getCardForEditOutput.Card,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}