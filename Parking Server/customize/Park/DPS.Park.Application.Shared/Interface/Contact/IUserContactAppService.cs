using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Contact.UserContact;

namespace DPS.Park.Application.Shared.Interface.Contact
{
    public interface IUserContactAppService: IApplicationService 
    {
        Task<PagedResultDto<GetUserContactForViewDto>> GetAll(GetAllUserContactInput input);
        
        Task<GetUserContactForEditOutput> GetUserContactForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditUserContactDto input);

        Task Delete(EntityDto input);
    }
}