using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Card.Card;
using Zero.Dto;

namespace DPS.Park.Application.Shared.Interface.Card
{
    public interface ICardAppService: IApplicationService 
    {
        Task<PagedResultDto<GetCardForViewDto>> GetAll(GetAllCardInput input);
        
        Task<GetCardForEditOutput> GetCardForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCardDto input);

        Task Delete(EntityDto input);
        
        Task<FileDto> GetCardsToExcel(GetAllCardInput input);
    }
}