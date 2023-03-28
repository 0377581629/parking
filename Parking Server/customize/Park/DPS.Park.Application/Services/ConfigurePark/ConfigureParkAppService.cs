using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using DPS.Park.Application.Shared.Dto.ConfigurePark;
using DPS.Park.Application.Shared.Interface.ConfigurePark;
using Zero;
using Zero.Authorization;
using Zero.Configuration;

namespace DPS.Park.Application.Services.ConfigurePark
{
    [AbpAuthorize(ParkPermissions.ConfigurePark)]
    public class ConfigureParkAppService: ZeroAppServiceBase,IConfigureParkAppService
    {
        private readonly ISettingManager _settingManager;

        public ConfigureParkAppService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }
        
        public async Task UpdateConfigurePark(ConfigureParkDto input)
        {
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.ApplyDecreasePercent,
                input.ApplyDecreasePercent.ToString().ToLowerInvariant());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.DecreasePercent,
                input.DecreasePercent.ToString().ToLowerInvariant());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.PhoneToSendMessage,
                input.PhoneToSendMessage.ToLowerInvariant());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.TotalSlotCount,
                input.TotalSlotCount.ToString().ToLowerInvariant());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.BalanceToSendEmail,
                input.BalanceToSendEmail.ToString().ToLowerInvariant());
        }
    }
}