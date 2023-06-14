using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using DPS.Park.Application.Shared.Dto.ConfigurePark;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Configuration;
using Zero.Web.Areas.Park.Models.ConfigurePark;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpAuthorize(ParkPermissions.ConfigurePark)]
    public class ConfigureParkController : ZeroControllerBase
    {
        private readonly ISettingManager _settingManager;

        public ConfigureParkController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = new ConfigureParkViewModel()
            {
                ConfigurePark = new ConfigureParkDto
                {
                    Name = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .Name),
                    Hotline = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .Hotline),
                    Address = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .Address),
                    SubAddress1 = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .SubAddress1),
                    SubAddress2 = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .SubAddress2),
                    Email = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                        .Email),
                    ApplyDecreasePercent =
                        await _settingManager.GetSettingValueAsync<bool>(AppSettings.ParkSettings
                            .ApplyDecreasePercent),
                    DecreasePercent =
                        await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings
                            .DecreasePercent),
                    PhoneToSendMessage =
                        await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                            .PhoneToSendMessage),
                    BalanceToSendEmail = 
                        await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings
                            .BalanceToSendEmail)
                }
            };
            return View(viewModel);
        }
    }
}