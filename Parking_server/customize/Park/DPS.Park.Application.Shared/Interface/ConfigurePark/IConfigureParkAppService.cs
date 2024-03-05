using System.Threading.Tasks;
using Abp.Application.Services;
using DPS.Park.Application.Shared.Dto.ConfigurePark;

namespace DPS.Park.Application.Shared.Interface.ConfigurePark
{
    public interface IConfigureParkAppService: IApplicationService
    {
        Task<ConfigureParkDto> Get();
        Task UpdateConfigurePark(ConfigureParkDto input);
    }
}