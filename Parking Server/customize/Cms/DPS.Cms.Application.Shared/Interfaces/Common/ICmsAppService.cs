using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Cms.Application.Shared.Dto.ImageBlockGroup;
using DPS.Cms.Application.Shared.Dto.Common;
using DPS.Cms.Application.Shared.Dto.PageLayout;
using DPS.Cms.Application.Shared.Dto.Widget;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DPS.Cms.Application.Shared.Interfaces.Common
{
    public interface ICmsAppService : IApplicationService
    {
        #region Advertisement

        Task<List<ImageBlockGroupDto>> GetAllImageBlockGroup();

        Task<List<SelectListItem>> GetAllImageBlockGroupDropDown(int? current = default);

        Task<PagedResultDto<ImageBlockGroupDto>> GetPagedImageBlockGroups(CmsInput input);

        #endregion
        
        #region Widget
        Task<WidgetDto> GetWidget(EntityDto input);
        Task<List<WidgetDto>> GetAllWidget();
        #endregion
        
        #region Page Layout
        Task<List<PageLayoutDto>> GetAllPageLayout();

        Task<List<SelectListItem>> GetAllPageLayoutDropDown(int? current = default);

        Task<PagedResultDto<PageLayoutDto>> GetPagedPageLayouts(CmsInput input);
        #endregion
    }
}