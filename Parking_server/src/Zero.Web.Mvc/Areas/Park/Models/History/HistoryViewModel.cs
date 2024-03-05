using System;

namespace Zero.Web.Areas.Park.Models.History
{
    public class HistoryViewModel
    {
        public string FilterText { get; set; }
        
        public DateTime FromDate { get; set; }
        
        public DateTime ToDate { get; set; }
    }
}