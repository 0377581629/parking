using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Student;

namespace DPS.Park.Application.Shared.Interface.Student
{
    public interface IStudentAppService : IApplicationService
    {
        Task<PagedResultDto<GetStudentForViewDto>> GetAll(GetAllStudentInput input);

        Task<GetStudentForEditOutput> GetStudentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditStudentDto input);

        Task Delete(EntityDto input);
    }
}