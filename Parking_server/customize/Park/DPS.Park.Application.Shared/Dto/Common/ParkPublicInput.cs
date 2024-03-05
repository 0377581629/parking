using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Common
{
    public class ParkPublicInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        #region Order

        public string OrderCode { get; set; }

        #endregion

        #region User
        
        public long? UserId { get; set; }
        
        #endregion

        #region Student

        public int? StudentId { get; set; }

        #endregion
    }
}