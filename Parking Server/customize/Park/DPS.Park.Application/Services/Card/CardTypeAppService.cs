using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Card.CardType;
using DPS.Park.Application.Shared.Interface.Card;
using DPS.Park.Core.Card;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Card
{
    [AbpAuthorize(ParkPermissions.CardType)]
    public class CardTypeAppService: ZeroAppServiceBase,ICardTypeAppService
    {
        private readonly IRepository<CardType> _cardTypeRepository;

        public CardTypeAppService(IRepository<CardType> cardTypeRepository)
        {
            _cardTypeRepository = cardTypeRepository;
        }
        
        private IQueryable<CardTypeDto> CardTypeQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _cardTypeRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter), e => e.Name.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new CardTypeDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Name = obj.Name,
                    Note = obj.Note
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllCardTypeInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetCardTypeForViewDto>> GetAll(GetAllCardTypeInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };
            var objQuery = CardTypeQuery(queryInput);

            var pagedAndFilteredCardType = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredCardType
                select new GetCardTypeForViewDto
                {
                    CardType = ObjectMapper.Map<CardTypeDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetCardTypeForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.CardType_Edit)]
        public async Task<GetCardTypeForEditOutput> GetCardTypeForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var cardType = await CardTypeQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetCardTypeForEditOutput
            {
                CardType = ObjectMapper.Map<CreateOrEditCardTypeDto>(cardType)
            };
            return output;
        }
        
        public async Task CreateOrEdit(CreateOrEditCardTypeDto input)
        {
            input.TenantId = AbpSession.TenantId;
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(ParkPermissions.CardType_Create)]
        protected virtual async Task Create(CreateOrEditCardTypeDto input)
        {
            var obj = ObjectMapper.Map<CardType>(input);
            await _cardTypeRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.CardType_Edit)]
        protected virtual async Task Update(CreateOrEditCardTypeDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _cardTypeRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.CardType_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _cardTypeRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _cardTypeRepository.DeleteAsync(input.Id);
        }
    }
}