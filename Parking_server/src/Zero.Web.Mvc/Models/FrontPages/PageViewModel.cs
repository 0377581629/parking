using System.Collections.Generic;
using System.Linq;
using DPS.Cms.Application.Shared.Dto.Page;
using DPS.Cms.Application.Shared.Dto.PageLayout;

namespace Zero.Web.Models.FrontPages
{
    public class PageViewModel
    {
        public PageDto Page { get; set; }
        
        public List<PageLayoutBlockDto> Blocks { get; set; }
        
        public List<PageWidgetDto> Widgets { get; set; }

        public bool IsValid => Page != null && Blocks != null && Blocks.Any() && Widgets != null && Widgets.Any();
    }
}