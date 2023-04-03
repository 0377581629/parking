using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.History;
using DPS.Park.Application.Shared.Interface.History;
using DPS.Park.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Web.Areas.Park.Models.History;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.History)]
    public class HistoryController: ZeroControllerBase
    {
        private readonly IHistoryAppService _historyAppService;

        public HistoryController(IHistoryAppService historyAppService)
        {
            _historyAppService = historyAppService;
        }
        
        public ActionResult Index()
        {
            var viewModel = new HistoryViewModel
            {
                FilterText = "",
                FromDate = DateTime.Today,
                ToDate = DateTime.Today
            };
            return View(viewModel);
        }
        
        [AbpMvcAuthorize(ParkPermissions.History_Create, ParkPermissions.History_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetHistoryForEditOutput getHistoryForEditOutput;

            if (id.HasValue)
            {
                getHistoryForEditOutput = await _historyAppService.GetHistoryForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getHistoryForEditOutput = new GetHistoryForEditOutput
                {
                    History = new CreateOrEditHistoryDto()
                    {
                        Time = DateTime.Now
                    }
                };
            }

            var viewModel = new CreateOrEditHistoryViewModel()
            {
                History = getHistoryForEditOutput.History,
                ListHistoryType = ParkHelper.ListHistoryType(0, LocalizationSource)
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
        
        public async Task<ActionResult> ViewModal(int id)
        {
            var getHistoryForEditOutput =
                await _historyAppService.GetHistoryForEdit(new EntityDto { Id = id });
            
            var viewModel = new CreateOrEditHistoryViewModel
            {
                History = getHistoryForEditOutput.History,
            };

            return PartialView("_ViewModal", viewModel);
        }
    }
}