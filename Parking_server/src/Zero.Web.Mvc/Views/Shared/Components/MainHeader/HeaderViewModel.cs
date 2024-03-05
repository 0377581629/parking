using System.Collections.Generic;
using Abp.Localization;
using DPS.Cms.Application.Shared.Dto.Menu;
using DPS.Park.Application.Shared.Dto.ConfigurePark;
using Zero.Sessions.Dto;

namespace Zero.Web.Views.Shared.Components.MainHeader
{
    public class HeaderViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformation { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public LanguageInfo CurrentLanguage { get; set; }

        public string AdminWebSiteRootAddress { get; set; }

        public string WebSiteRootAddress { get; set; }

        public string GetShownLoginName => LoginInformation.User.UserName;

        public ConfigureParkDto ConfigurePark { get; set; }
        
        public List<MenuDto> Menus { get; set; }
    }
}