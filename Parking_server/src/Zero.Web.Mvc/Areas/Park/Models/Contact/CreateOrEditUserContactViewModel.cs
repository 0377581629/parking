using DPS.Park.Application.Shared.Dto.Contact.UserContact;

namespace Zero.Web.Areas.Park.Models.Contact
{
    public class CreateOrEditUserContactViewModel
    {
        public CreateOrEditUserContactDto UserContact { get; set; }

        public bool IsEditMode => UserContact.Id.HasValue;
    }
}