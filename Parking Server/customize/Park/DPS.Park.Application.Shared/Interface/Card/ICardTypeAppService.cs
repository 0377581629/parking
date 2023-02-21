using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Card.CardType;

namespace DPS.Park.Application.Shared.Interface.Card
{
    public interface ICardTypeAppService: IApplicationService 
    {
        Task<PagedResultDto<GetCardTypeForViewDto>> GetAll(GetAllCardTypeInput input);
        
        Task<GetCardTypeForEditOutput> GetCardTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCardTypeDto input);

        Task Delete(EntityDto input);
    }
}