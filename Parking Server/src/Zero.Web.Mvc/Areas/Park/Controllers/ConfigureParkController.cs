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
                    ApplyDecreasePercent =
                        await _settingManager.GetSettingValueAsync<bool>(AppSettings.ParkSettings
                            .ApplyDecreasePercent),
                    DecreasePercent =
                        await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings
                            .DecreasePercent),
                    PhoneToSendMessage =
                        await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                            .PhoneToSendMessage),
                    TotalSlotCount =
                        await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings
                            .TotalSlotCount),
                    MonthlyFare = 
                        await _settingManager.GetSettingValueAsync<int>(AppSettings.ParkSettings
                            .MonthlyFare)
                }
            };
            return View(viewModel);
        }
    }
}