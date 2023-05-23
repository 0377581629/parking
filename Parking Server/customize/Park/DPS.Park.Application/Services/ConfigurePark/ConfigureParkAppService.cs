using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Runtime.Session;
using DPS.Park.Application.Shared.Dto.ConfigurePark;
using DPS.Park.Application.Shared.Interface.ConfigurePark;
using Zero;
using Zero.Authorization;
using Zero.Configuration;

namespace DPS.Park.Application.Services.ConfigurePark
{
    [AbpAuthorize(ParkPermissions.ConfigurePark)]
    public class ConfigureParkAppService : ZeroAppServiceBase, IConfigureParkAppService
    {
        private readonly ISettingManager _settingManager;

        public ConfigureParkAppService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task UpdateConfigurePark(ConfigureParkDto input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.Name, input.Name);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.Hotline, input.Hotline);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.Address, input.Address);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.SubAddress1, input.SubAddress1);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.SubAddress2, input.SubAddress2);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.Email, input.Email);
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.ApplyDecreasePercent,
                    input.ApplyDecreasePercent.ToString().ToLowerInvariant());
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.DecreasePercent,
                    input.DecreasePercent.ToString().ToLowerInvariant());
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.PhoneToSendMessage,
                    input.PhoneToSendMessage.ToLowerInvariant());
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.TotalSlotCount,
                    input.TotalSlotCount.ToString().ToLowerInvariant());
                await _settingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(),
                    AppSettings.ParkSettings.BalanceToSendEmail,
                    input.BalanceToSendEmail.ToString().ToLowerInvariant());
            }
            else
            {
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.Name, input.Name);
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.Hotline, input.Hotline);
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.Address, input.Address);
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.SubAddress1, input.SubAddress1);
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.SubAddress2, input.SubAddress2);
                await _settingManager.ChangeSettingForApplicationAsync(AppSettings.ParkSettings.Email, input.Email);
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

        [AbpAllowAnonymous]
        public async Task<ConfigureParkDto> Get()
        {
            ConfigureParkDto res;
            if (AbpSession.TenantId.HasValue)
            {
                res = new ConfigureParkDto
                {
                    Name = await _settingManager.GetSettingValueForTenantAsync(AppSettings.ParkSettings.Name,
                        AbpSession.GetTenantId()),
                    Hotline = await _settingManager.GetSettingValueForTenantAsync(AppSettings.ParkSettings.Hotline,
                        AbpSession.GetTenantId()),
                    SubAddress1 = await _settingManager.GetSettingValueForTenantAsync(AppSettings.ParkSettings.Address,
                        AbpSession.GetTenantId()),
                    SubAddress2 = await _settingManager.GetSettingValueForTenantAsync(AppSettings.ParkSettings.Address,
                        AbpSession.GetTenantId()),
                    Email = await _settingManager.GetSettingValueForTenantAsync(AppSettings.ParkSettings.Email,
                        AbpSession.GetTenantId()),
                };
            }
            else
            {
                res = new ConfigureParkDto
                {
                    Name = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.ParkSettings.Name),
                    Hotline =
                        await _settingManager.GetSettingValueForApplicationAsync(AppSettings.ParkSettings.Hotline),
                    Address = 
                        await _settingManager.GetSettingValueForApplicationAsync(AppSettings.ParkSettings.Address),
                    SubAddress1 = 
                        await _settingManager.GetSettingValueForApplicationAsync(AppSettings.ParkSettings.SubAddress1),
                    SubAddress2 = 
                        await _settingManager.GetSettingValueForApplicationAsync(AppSettings.ParkSettings.SubAddress2),
                };
            }

            return res;
        }
    }
}