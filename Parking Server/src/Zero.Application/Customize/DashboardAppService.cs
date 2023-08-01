using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.UI;
using DPS.Park.Core.Card;
using DPS.Park.Core.History;
using DPS.Park.Core.Vehicle;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Zero;
using Zero.Configuration;
using Zero.Customize.Dashboard;
using Zero.Customize.Dto.Dashboard.Config;
using Zero.Customize.Dto.Dashboard.DashboardWidget;
using Zero.Customize.Dto.Dashboard.Function;
using Zero.Customize.Interfaces;

namespace Zero.Customize
{
    [AbpAuthorize]
    public class DashboardAppService : ZeroAppServiceBase, IDashboardAppService
    {
        private readonly IZeroAppService _zeroAppService;
        private readonly IRepository<DashboardWidget> _dashboardWidgetRepository;
        private readonly IRepository<EditionDashboardWidget> _editionDashboardWidgetRepository;
        private readonly IRepository<RoleDashboardWidget> _roleDashboardWidgetRepository;
        private readonly ISettingStore _settingStore;

        private readonly IRepository<History> _historyRepository;
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<CardType> _cardTypeRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public DashboardAppService(IRepository<DashboardWidget> dashboardWidgetRepository,
            IRepository<EditionDashboardWidget> editionDashboardWidgetRepository,
            IRepository<RoleDashboardWidget> roleDashboardWidgetRepository, IZeroAppService zeroAppService,
            ISettingStore settingStore, IRepository<History> historyRepository, IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _dashboardWidgetRepository = dashboardWidgetRepository;
            _editionDashboardWidgetRepository = editionDashboardWidgetRepository;
            _roleDashboardWidgetRepository = roleDashboardWidgetRepository;
            _zeroAppService = zeroAppService;
            _settingStore = settingStore;
            _historyRepository = historyRepository;
            _cardRepository = cardRepository;
            _cardTypeRepository = cardTypeRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        #region Default

        private IQueryable<DashboardWidgetDto> DashboardWidgetQuery()
        {
            var query = from o in _dashboardWidgetRepository.GetAll()
                select new DashboardWidgetDto()
                {
                    Id = o.Id,
                    WidgetId = o.WidgetId,
                    Name = o.Name,
                    Description = o.Description,

                    Width = o.Width,
                    Height = o.Height,
                    PositionX = o.PositionX,
                    PositionY = o.PositionY,

                    ViewName = o.ViewName,
                    JsPath = o.JsPath,
                    CssPath = o.CssPath,

                    Filters = o.Filters,
                    IsDefault = o.IsDefault
                };

            return query;
        }

        public async Task<List<DashboardWidgetDto>> GetAllDashboardWidget()
        {
            return await DashboardWidgetQuery().ToListAsync();
        }

        public async Task<List<DashboardWidgetDto>> GetAllDashboardWidgetByEdition(int editionId)
        {
            var grantedByEdition =
                await _editionDashboardWidgetRepository.GetAllListAsync(o => o.EditionId == editionId);
            var allWidgets = await DashboardWidgetQuery().ToListAsync();
            return allWidgets.Where(o => grantedByEdition.Any(g => g.DashboardWidgetId == o.Id)).ToList();
        }

        public async Task<List<DashboardWidgetDto>> GetAllDashboardWidgetByRole(int roleId)
        {
            var grantedByEdition = await _roleDashboardWidgetRepository.GetAllListAsync(o => o.RoleId == roleId);
            var allWidgets = await DashboardWidgetQuery().ToListAsync();
            return allWidgets.Where(o => grantedByEdition.Any(g => g.DashboardWidgetId == o.Id)).ToList();
        }

        public async Task<List<DashboardWidgetDto>> GetAllDashboardWidgetByRoles(List<int> roleIds)
        {
            var grantedByEdition =
                await _roleDashboardWidgetRepository.GetAllListAsync(o => roleIds.Contains(o.RoleId));
            var allWidgets = await DashboardWidgetQuery().ToListAsync();
            return allWidgets.Where(o => grantedByEdition.Any(g => g.DashboardWidgetId == o.Id)).ToList();
        }

        private static string SettingName => "App.DashboardCustomization.Configuration.Mvc";

        public async Task<Dto.Dashboard.Config.Dashboard> GetUserDashboard()
        {
            return GetDashboard(await GetDashboardFromSettings());
        }

        public async Task SavePage(SavePageInput input)
        {
            var dashboards = await GetDashboardFromSettings();
            var dashboard = GetDashboard(dashboards);

            foreach (var inputPage in input.Pages)
            {
                var page = dashboard.Pages.FirstOrDefault(p => p.Id == inputPage.Id);
                var pageIndex = dashboard.Pages.IndexOf(page);

                dashboard.Pages.RemoveAt(pageIndex);

                if (page != null)
                {
                    inputPage.Name = page.Name;
                    dashboard.Pages.Insert(pageIndex, inputPage);
                }
            }

            await SaveSetting(dashboards);
        }

        public async Task RenamePage(RenamePageInput input)
        {
            var dashboards = await GetDashboardFromSettings();
            var dashboard = GetDashboard(dashboards);

            var page = dashboard.Pages.FirstOrDefault(p => p.Id == input.Id);
            if (page == null)
            {
                return;
            }

            page.Name = input.Name;

            await SaveSetting(dashboards);
        }

        public async Task<AddNewPageOutput> AddNewPage(AddNewPageInput input)
        {
            var dashboards = await GetDashboardFromSettings();
            var dashboard = GetDashboard(dashboards);

            var page = new Page
            {
                Name = input.Name,
                Widgets = new List<DashboardWidgetDto>(),
            };

            dashboard.Pages.Add(page);
            await SaveSetting(dashboards);

            return new AddNewPageOutput { PageId = page.Id };
        }

        public async Task DeletePage(DeletePageInput input)
        {
            var dashboards = await GetDashboardFromSettings();
            var dashboard = GetDashboard(dashboards);
            dashboard.Pages.RemoveAll(p => p.Id == input.Id);
            await SaveSetting(dashboards);
        }

        public async Task<DashboardWidgetDto> AddWidget(AddWidgetInput input)
        {
            var dashboards = await GetDashboardFromSettings();
            var dashboard = GetDashboard(dashboards);

            var page = dashboard.Pages.Single(p => p.Id == input.PageId);

            var widget = new DashboardWidgetDto
            {
                WidgetId = input.WidgetId,
                Height = input.Height,
                Width = input.Width,
                PositionX = 0,
                PositionY = CalculatePositionY(page.Widgets)
            };

            page.Widgets.Add(widget);

            await SaveSetting(dashboards);
            return widget;
        }

        private Dto.Dashboard.Config.Dashboard GetDashboard(List<Dto.Dashboard.Config.Dashboard> dashboards)
        {
            var dashboard = dashboards.FirstOrDefault();
            if (dashboard == null)
            {
                throw new UserFriendlyException(L("UnknownDashboard", "Dashboard"));
            }

            return dashboard;
        }

        private byte CalculatePositionY(List<DashboardWidgetDto> widgets)
        {
            if (widgets == null || !widgets.Any())
            {
                return 0;
            }

            return (byte)widgets.Max(w => w.PositionY + w.Height);
        }

        private async Task<List<Dto.Dashboard.Config.Dashboard>> GetDashboardFromSettings()
        {
            var allUserSettings = await _settingStore.GetAllListAsync(AbpSession.TenantId, AbpSession.UserId);
            if (!allUserSettings.Any(o => o.Name.Equals(SettingName)))
            {
                var rolesIds = await _zeroAppService.GetCurrentRoleIds();
                var defaultWidgets = (await GetAllDashboardWidgetByRoles(rolesIds)).Where(o => o.IsDefault).ToList();
                return new List<Dto.Dashboard.Config.Dashboard>()
                {
                    new()
                    {
                        DashboardName = "Dashboard",
                        Pages = new List<Page>()
                        {
                            new(ZeroDashboardCustomizationConsts.DefaultDashboardPageId)
                            {
                                Name = ZeroDashboardCustomizationConsts.DefaultPageName,
                                Widgets = defaultWidgets.Any() ? defaultWidgets : new List<DashboardWidgetDto>()
                            }
                        }
                    }
                };
            }

            return JsonConvert.DeserializeObject<List<Dto.Dashboard.Config.Dashboard>>(allUserSettings
                .First(o => o.Name == SettingName).Value);
        }

        private async Task SaveSetting(List<Dto.Dashboard.Config.Dashboard> dashboards)
        {
            var value = JsonConvert.SerializeObject(dashboards);

            var allUserSettings = await _settingStore.GetAllListAsync(AbpSession.TenantId, AbpSession.UserId);
            var currentSetting = allUserSettings.FirstOrDefault(o => o.Name.Equals(SettingName));
            if (currentSetting == null)
            {
                await _settingStore.CreateAsync(new SettingInfo(AbpSession.TenantId, AbpSession.UserId, SettingName,
                    value));
            }
            else
            {
                currentSetting.Value = value;
                await _settingStore.UpdateAsync(currentSetting);
            }
        }

        #endregion

        #region Custom

        public async Task<List<WeeklyParkingAmountOutput>> GetParkingAmountByWeek()
        {
            var today = DateTime.Today;
            var dayOffWeek = (int)today.DayOfWeek;
            var firstDayOfThisWeekInYear = today.DayOfYear - dayOffWeek + 1;
            var res = new List<WeeklyParkingAmountOutput>();

            for (var day = 1; day <= dayOffWeek; day++)
            {
                var parkingAmountOfDay = await _historyRepository.CountAsync(o => !o.IsDeleted &&
                    o.TenantId == AbpSession.TenantId &&
                    o.Time.DayOfYear == firstDayOfThisWeekInYear + day - 1);

                res.Add(new WeeklyParkingAmountOutput()
                {
                    Day = day != 6 ? $"{L("DayOfWeek")} {day + 1}" : L("Sunday"),
                    ParkingAmount = parkingAmountOfDay
                });
            }

            return res;
        }

        public async Task<List<WeeklyParkingRevenueOutput>> GetParkingRevenueByWeek()
        {
            var today = DateTime.Today;
            var dayOffWeek = (int)today.DayOfWeek;
            var firstDayOfThisWeekInYear = today.DayOfYear - dayOffWeek + 1;
            var res = new List<WeeklyParkingRevenueOutput>();

            for (var day = 1; day <= dayOffWeek; day++)
            {
                var historiesOfDay = await _historyRepository.GetAll().Where(o => !o.IsDeleted &&
                    o.TenantId == AbpSession.TenantId &&
                    o.Time.DayOfYear == firstDayOfThisWeekInYear + day - 1).ToListAsync();

                var parkingRevenueOfDay = historiesOfDay.Select(o => o.Price).Sum();

                res.Add(new WeeklyParkingRevenueOutput()
                {
                    Day = day != 6 ? $"{L("DayOfWeek")} {day + 1}" : L("Sunday"),
                    ParkingRevenue = parkingRevenueOfDay ?? 0
                });
            }

            return res;
        }

        private IQueryable<RatioByCardTypeOutput> CurrentCardTypeQuery()
        {
            var query = from o in _cardTypeRepository.GetAll()
                    .Where(o => o.TenantId == AbpSession.TenantId && !o.IsDeleted)
                join card in _cardRepository.GetAll()
                    on o.Id equals card.CardTypeId
                select new
                {
                    CardTypeId = o.Id,
                    CardTypeName = o.Name,
                };
            var res = query.GroupBy(e => new { e.CardTypeId, e.CardTypeName }).Select(
                e => new RatioByCardTypeOutput
                {
                    Count = e.Count(),
                    CardTypeName = e.Key.CardTypeName
                });
            return res;
        }

        public async Task<List<RatioByCardTypeOutput>> GetRatioByCardType()
        {
            var res = CurrentCardTypeQuery().ToList();
            var totalCard = await _cardRepository.CountAsync(o => o.TenantId == AbpSession.TenantId && !o.IsDeleted);

            foreach (var item in res)
            {
                item.Ratio = item.Count / totalCard * 100;
                item.CardTypeName = item.CardTypeName;
            }

            return res;
        }

        private IQueryable<RatioByVehicleTypeOutput> CurrentVehicleTypeQuery()
        {
            var query = from o in _vehicleTypeRepository.GetAll()
                    .Where(o => o.TenantId == AbpSession.TenantId && !o.IsDeleted)
                join card in _cardRepository.GetAll()
                    on o.Id equals card.VehicleTypeId
                select new
                {
                    VehicleTypeId = o.Id,
                    VehicleTypeName = o.Name,
                };
            var res = query.GroupBy(e => new { e.VehicleTypeId, e.VehicleTypeName }).Select(
                e => new RatioByVehicleTypeOutput()
                {
                    Count = e.Count(),
                    VehicleTypeName = e.Key.VehicleTypeName
                });
            return res;
        }

        public async Task<List<RatioByVehicleTypeOutput>> GetRatioByVehicleType()
        {
            var res = CurrentVehicleTypeQuery().ToList();
            var totalCard = await _cardRepository.CountAsync(o => o.TenantId == AbpSession.TenantId && !o.IsDeleted);

            foreach (var item in res)
            {
                item.Ratio = item.Count / totalCard * 100;
                item.VehicleTypeName = item.VehicleTypeName;
            }

            return res;
        }

        #endregion
    }
}