using System.Collections.Generic;
using DPS.Park.Application.Shared.Dto.Fare;

namespace DPS.Park.Application.Shared.Dto.Sync
{
    public class BaseInfoDto
    {
        public List<FareDto> TicketPrices { get; set; }
    }
}