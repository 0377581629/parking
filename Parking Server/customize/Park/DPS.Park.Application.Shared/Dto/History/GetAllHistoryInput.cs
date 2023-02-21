using System;
using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.History
{
    public class GetAllHistoryInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }
    }
}