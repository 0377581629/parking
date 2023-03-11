using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using DPS.Park.Application.Shared.Dto.Resident;
using DPS.Park.Application.Shared.Dto.Resident.ResidentCard;
using DPS.Park.Application.Shared.Interface.Resident;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Web.Areas.Park.Models.Resident;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Resident)]
    public class ResidentController : ZeroControllerBase
    {
        private readonly IResidentAppService _residentAppService;

        public ResidentController(IResidentAppService residentAppService)
        {
            _residentAppService = residentAppService;
        }

        public ActionResult Index()
        {
            var viewModel = new ResidentViewModel()
            {
                FilterText = "",
            };
            return View(viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Resident_Create, ParkPermissions.Resident_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetResidentForEditOutput getResidentForEditOutput;

            if (id.HasValue)
            {
                getResidentForEditOutput = await _residentAppService.GetResidentForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getResidentForEditOutput = new GetResidentForEditOutput()
                {
                    Resident = new CreateOrEditResidentDto()
                    {
                        IsActive = true,
                        IsPaid = false
                    }
                };
            }

            var viewModel = new CreateOrEditResidentViewModel()
            {
                Resident = getResidentForEditOutput.Resident
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Resident_Create, ParkPermissions.Resident_Edit)]
        public PartialViewResult NewResidentDetail()
        {
            var res = new ResidentCardDto();
            return PartialView("Components/Details/_ResidentDetail", res);
        }
    }
}