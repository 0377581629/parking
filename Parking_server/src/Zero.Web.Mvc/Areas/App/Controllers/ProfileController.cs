﻿using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Configuration;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization.Users.Profile;
using Zero.Configuration;
using Zero.Timing;
using Zero.Timing.Dto;
using Zero.Web.Areas.App.Models.Profile;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ProfileController : ZeroControllerBase
    {
        private readonly IProfileAppService _profileAppService;
        private readonly ITimingAppService _timingAppService;
        private readonly ITenantCache _tenantCache;

        public ProfileController(
            IProfileAppService profileAppService,
            ITimingAppService timingAppService, 
            ITenantCache tenantCache)
        {
            _profileAppService = profileAppService;
            _timingAppService = timingAppService;
            _tenantCache = tenantCache;
        }

        public async Task<PartialViewResult> MySettingsModal()
        {
            var output = await _profileAppService.GetCurrentUserProfileForEdit();
            var viewModel = ObjectMapper.Map<MySettingsViewModel>(output);
            viewModel.TimezoneItems = await _timingAppService.GetTimezoneComboboxItems(new GetTimezoneComboboxItemsInput
            {
                DefaultTimezoneScope = SettingScopes.User,
                SelectedTimezoneId = output.Timezone
            });
            viewModel.SmsVerificationEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.SmsVerificationEnabled);

            return PartialView("_MySettingsModal", viewModel);
        }

        public PartialViewResult ChangePictureModal()
        {
            return PartialView("_ChangePictureModal");
        }

        public PartialViewResult ChangePasswordModal()
        {
            return PartialView("_ChangePasswordModal");
        }

        

        public PartialViewResult SmsVerificationModal()
        {
            return PartialView("_SmsVerificationModal");
        }


        public PartialViewResult LinkedAccountsModal()
        {
            return PartialView("_LinkedAccountsModal");
        }

        public PartialViewResult LinkAccountModal()
        {
            ViewBag.TenancyName = GetTenancyNameOrNull();
            return PartialView("_LinkAccountModal");
        }

        public PartialViewResult UserDelegationsModal()
        {
            return PartialView("_UserDelegationsModal");
        }

        public PartialViewResult CreateNewUserDelegationModal()
        {
            return PartialView("_CreateNewUserDelegationModal");
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }
    }
}