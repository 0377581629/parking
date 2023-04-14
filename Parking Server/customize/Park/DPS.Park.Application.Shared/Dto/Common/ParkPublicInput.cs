using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Common
{
    public class ParkPublicInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        #region Order

        public string OrderCode { get; set; }

        #endregion
    }
}