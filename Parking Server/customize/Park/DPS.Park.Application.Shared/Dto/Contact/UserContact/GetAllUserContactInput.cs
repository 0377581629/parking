using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Contact.UserContact
{
    public class GetAllUserContactInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}