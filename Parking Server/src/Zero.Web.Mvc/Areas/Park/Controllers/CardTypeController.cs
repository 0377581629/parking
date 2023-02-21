using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Card.CardType;
using DPS.Park.Application.Shared.Interface.Card;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Web.Areas.Park.Models.CardType;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.CardType)]
    public class CardTypeController: ZeroControllerBase
    {
        private readonly ICardTypeAppService _cardTypeAppService;

        public CardTypeController(ICardTypeAppService cardTypeAppService)
        {
            _cardTypeAppService = cardTypeAppService;
        }
        
        public ActionResult Index()
        {
            var viewModel = new CardTypeViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }
        
        [AbpMvcAuthorize(ParkPermissions.CardType_Create, ParkPermissions.CardType_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCardTypeForEditOutput getCardTypeForEditOutput;

            if (id.HasValue)
            {
                getCardTypeForEditOutput = await _cardTypeAppService.GetCardTypeForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getCardTypeForEditOutput = new GetCardTypeForEditOutput
                {
                    CardType = new CreateOrEditCardTypeDto()
                };
            }

            var viewModel = new CreateOrEditCardTypeViewModel()
            {
                CardType = getCardTypeForEditOutput.CardType,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}