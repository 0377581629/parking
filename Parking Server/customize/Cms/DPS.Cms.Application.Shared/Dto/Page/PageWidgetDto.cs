using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DPS.Cms.Application.Shared.Dto.ImageBlockGroup;
using DPS.Cms.Application.Shared.Dto.MenuGroup;
using DPS.Cms.Application.Shared.Dto.Post;

namespace DPS.Cms.Application.Shared.Dto.Page
{
	public class PageWidgetDto : EntityDto<int?>
	{
		public string UniqueId { get; set; }
		
		public string PageBlockColumnId { get; set; }
		
		public int PageId { get; set; }
		
		public int? PageThemeId { get; set; }
		
		public string PageThemeCode { get; set; }
		
		public string PageThemeName { get; set; }
        
		#region Widget
		
		public int? WidgetPageThemeId { get; set; }
		
		public string WidgetPageThemeCode { get; set; }
		
		public string WidgetPageThemeName { get; set; }
		
		public int WidgetId { get; set; }
		
		public string WidgetName { get; set; }
		
		public string WidgetActionName { get; set; }
		
		public string WidgetControllerName { get; set; }
		
		public int WidgetContentType { get; set; }
		
		public int WidgetContentCount { get; set; }
		
		public string WidgetJsBundleUrl { get; set; }
        
		public string WidgetJsScript { get; set; }
        
		public string WidgetJsPlain { get; set; }
        
		public string WidgetCssBundleUrl { get; set; }
        
		public string WidgetCssScript { get; set; }
        
		public string WidgetCssPlain { get; set; }
		
		public int Order { get; set; }
		
		public List<PageWidgetDetailDto> Details { get; set; }
		#endregion
		
		#region Use for front pages
		
		public List<ImageBlockGroupDto> ListImageBlockGroup { get; set; }

		public List<MenuGroupDto> ListMenuGroup { get; set; }
		
		#endregion
		
		#region Post

		public PostDto Post { get; set; }
		public List<PostDto> ListPosts { get; set; }

		#region Post Listing Filter
		
		public string PostsSorting { get; set; }

		public string PostsFiltering { get; set; }

		public int PostsSkipCount { get; set; }

		public int PostsMaxResultCount { get; set; }

		public int PostsCount { get; set; }

		public int? PostCategoryId { get; set; }
		
		public int PostsTotalPage =>
			PostsMaxResultCount <= 0 ? 0 : (int)Math.Ceiling((double)PostsCount / PostsMaxResultCount);

		public int PostsCurrentPage => PostsMaxResultCount <= 0
			? 0
			: (int)Math.Ceiling((double)PostsSkipCount / PostsMaxResultCount);

		#endregion

		#endregion
	}
}