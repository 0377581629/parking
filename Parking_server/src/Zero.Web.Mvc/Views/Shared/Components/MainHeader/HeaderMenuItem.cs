using System.Collections.Generic;
using System.Linq;
using DPS.Cms.Application.Shared.Dto.Menu;

namespace Zero.Web.Views.Shared.Components.MainHeader
{
    public class HeaderMenuItem
    {
        public MenuDto Menu { get; set; }

        public List<MenuDto> ChildMenus => AllMenus.Where(o => o.ParentId == Menu.Id).ToList();
        
        public List<MenuDto> AllMenus { get; set; }
    }
}