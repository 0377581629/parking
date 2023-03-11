using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Resident;

namespace DPS.Park.Application.Shared.Interface.Resident
{
    public interface IResidentAppService : IApplicationService
    {
        Task<PagedResultDto<GetResidentForViewDto>> GetAll(GetAllResidentInput input);

        Task<GetResidentForEditOutput> GetResidentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditResidentDto input);

        Task Delete(EntityDto input);
    }
}