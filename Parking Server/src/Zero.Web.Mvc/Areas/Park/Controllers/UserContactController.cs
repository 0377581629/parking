using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using DPS.Park.Application.Shared.Dto.Contact.UserContact;
using DPS.Park.Application.Shared.Interface.Contact;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Web.Areas.Park.Models.Contact;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.UserContact)]
    public class UserContactController: ZeroControllerBase
    {
        private readonly IUserContactAppService _userContactAppService;

        public UserContactController(IUserContactAppService userContactAppService)
        {
            _userContactAppService = userContactAppService;
        }

        public ActionResult Index() => View(new UserContactViewModel {FilterText = ""});

        [AbpAuthorize(ParkPermissions.UserContact_Create, ParkPermissions.UserContact_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var getUserContactForEditOutput = new GetUserContactForEditOutput
            {
                UserContact = new CreateOrEditUserContactDto
                {
                    Code = StringHelper.ShortIdentity(),
                    IsActive = true
                }
            };

            if (id.HasValue) getUserContactForEditOutput = await _userContactAppService.GetUserContactForEdit(new EntityDto {Id = (int) id});
            return PartialView("_CreateOrEditModal", new CreateOrEditUserContactViewModel
            {
                UserContact = getUserContactForEditOutput.UserContact
            });
        }
    }
}