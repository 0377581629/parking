using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Resident.ResidentCard;

namespace DPS.Park.Application.Shared.Dto.Resident
{
    public class CreateOrEditResidentDto : FullAuditedEntityDto<int?>
    {
        public int? TenantId { get; set; }

        public string ApartmentNumber { get; set; }

        public string OwnerFullName { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerPhone { get; set; }

        public bool IsPaid { get; set; }

        public bool IsActive { get; set; }

        public List<ResidentCardDto> ResidentDetails { get; set; }
    }
}