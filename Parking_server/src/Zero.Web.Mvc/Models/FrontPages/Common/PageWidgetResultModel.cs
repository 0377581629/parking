using System;
using System.Collections.Generic;

namespace Zero.Web.Models.FrontPages.Common
{
    public class PageWidgetResultModel<T>
    {
	    public T Result { get; set; }
	    
	    public IReadOnlyList<T> Results { get; set; }
	    
	    #region Filter / Paging
        public string Filtering { get; set; }
		
        public int SkipCount { get; set; }
		
        public int MaxResultCount { get; set; }
		
        public int AllResultsCount { get; set; }
		
        public int TotalPage => MaxResultCount<=0 ? 0 : (int) Math.Ceiling((double) AllResultsCount / MaxResultCount);

        public int CurrentPage => MaxResultCount <= 0 ? 0 : (int) Math.Ceiling((double) SkipCount / MaxResultCount);
        #endregion
    }
}