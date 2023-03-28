using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DPS.Park.Application.Shared.Dto.Student;
using DPS.Park.Application.Shared.Dto.Student.StudentCard;
using DPS.Park.Application.Shared.Interface.Student;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Web.Areas.Park.Models.Student;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Student)]
    public class StudentController : ZeroControllerBase
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        public ActionResult Index()
        {
            var viewModel = new StudentViewModel()
            {
                FilterText = "",
            };
            return View(viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Student_Create, ParkPermissions.Student_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetStudentForEditOutput getStudentForEditOutput;

            if (id.HasValue)
            {
                getStudentForEditOutput = await _studentAppService.GetStudentForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getStudentForEditOutput = new GetStudentForEditOutput()
                {
                    Student = new CreateOrEditStudentDto()
                    {
                        Code = StringHelper.ShortIdentity(),
                        DoB = DateTime.Today,
                        IsActive = true,
                    }
                };
            }

            var viewModel = new CreateOrEditStudentViewModel()
            {
                Student = getStudentForEditOutput.Student
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Student_Create, ParkPermissions.Student_Edit)]
        public PartialViewResult NewStudentDetail()
        {
            var res = new StudentCardDto();
            return PartialView("Components/Details/_StudentDetail", res);
        }
    }
}