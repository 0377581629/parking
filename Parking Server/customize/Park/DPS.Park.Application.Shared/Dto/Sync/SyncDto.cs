using System.Collections.Generic;
using DPS.Park.Application.Shared.Dto.History;

namespace DPS.Park.Application.Shared.Dto.Sync
{
    public class SyncDto
    {
        public int? TenantId { get; set; }
        
        public List<CreateOrEditHistoryDto> Details { get; set; }
    }
}