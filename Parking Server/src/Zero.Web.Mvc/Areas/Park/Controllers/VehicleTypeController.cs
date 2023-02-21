using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;
using DPS.Park.Application.Shared.Interface.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Web.Areas.Park.Models.VehicleType;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.VehicleType)]
    public class VehicleTypeController: ZeroControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeAppService;

        public VehicleTypeController(IVehicleTypeAppService vehicleTypeAppService)
        {
            _vehicleTypeAppService = vehicleTypeAppService;
        }
        
        public ActionResult Index()
        {
            var viewModel = new VehicleTypeViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }
        
        [AbpMvcAuthorize(ParkPermissions.VehicleType_Create, ParkPermissions.VehicleType_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetVehicleTypeForEditOutput getVehicleTypeForEditOutput;

            if (id.HasValue)
            {
                getVehicleTypeForEditOutput = await _vehicleTypeAppService.GetVehicleTypeForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getVehicleTypeForEditOutput = new GetVehicleTypeForEditOutput
                {
                    VehicleType = new CreateOrEditVehicleTypeDto()
                };
            }

            var viewModel = new CreateOrEditVehicleTypeViewModel()
            {
                VehicleType = getVehicleTypeForEditOutput.VehicleType,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}