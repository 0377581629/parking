using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Fare;
using DPS.Park.Application.Shared.Interface.Fare;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Web.Areas.Park.Models.Fare;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Fare)]
    public class FareController: ZeroControllerBase
    {
        private readonly IFareAppService _fareAppService;

        public FareController(IFareAppService fareAppService)
        {
            _fareAppService = fareAppService;
        }
        
        public ActionResult Index()
        {
            var viewModel = new FareViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }
        
        [AbpMvcAuthorize(ParkPermissions.Fare_Create, ParkPermissions.Fare_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetFareForEditOutput getFareForEditOutput;

            if (id.HasValue)
            {
                getFareForEditOutput = await _fareAppService.GetFareForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getFareForEditOutput = new GetFareForEditOutput
                {
                    Fare = new CreateOrEditFareDto
                    {
                        DayOfWeekStart = (int)DayOfWeek.Monday,
                        DayOfWeekEnd = (int)DayOfWeek.Friday
                    }
                };
            }

            var viewModel = new CreateOrEditFareViewModel
            {
                Fare = getFareForEditOutput.Fare,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}