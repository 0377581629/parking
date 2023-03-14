using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Exporting.Card;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Interface.Card;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;
using Zero.Dto;

namespace DPS.Park.Application.Services.Card
{
    [AbpAuthorize(ParkPermissions.Card)]
    public class CardAppService : ZeroAppServiceBase, ICardAppService
    {
        private readonly IRepository<Core.Card.Card> _cardRepository;
        private readonly ICardExcelExporter _cardExcelExporter;

        public CardAppService(IRepository<Core.Card.Card> cardRepository, ICardExcelExporter cardExcelExporter)
        {
            _cardRepository = cardRepository;
            _cardExcelExporter = cardExcelExporter;
        }

        private IQueryable<CardDto> CardQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _cardRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Code.Contains(input.Filter) || e.CardNumber.Contains(input.Filter) ||
                             e.Note.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new CardDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Code = obj.Code,
                    CardNumber = obj.CardNumber,
                    Note = obj.Note,
                    IsActive = obj.IsActive,

                    CardTypeId = obj.CardTypeId,
                    CardTypeName = obj.CardType.Name,

                    VehicleTypeId = obj.VehicleTypeId,
                    VehicleTypeName = obj.VehicleType.Name
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllCardInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetCardForViewDto>> GetAll(GetAllCardInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };
            var objQuery = CardQuery(queryInput);

            var pagedAndFilteredCard = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredCard
                select new GetCardForViewDto
                {
                    Card = ObjectMapper.Map<CardDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetCardForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.Card_Edit)]
        public async Task<GetCardForEditOutput> GetCardForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var card = await CardQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetCardForEditOutput
            {
                Card = ObjectMapper.Map<CreateOrEditCardDto>(card)
            };
            return output;
        }

        private async Task ValidateDataInput(CreateOrEditCardDto input)
        {
            var res = await _cardRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code.Equals(input.Code))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task CreateOrEdit(CreateOrEditCardDto input)
        {
            input.TenantId = AbpSession.TenantId;
            input.Code = input.Code.Replace(" ", "");

            await ValidateDataInput(input);

            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(ParkPermissions.Card_Create)]
        protected virtual async Task Create(CreateOrEditCardDto input)
        {
            var obj = ObjectMapper.Map<Core.Card.Card>(input);
            await _cardRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.Card_Edit)]
        protected virtual async Task Update(CreateOrEditCardDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _cardRepository.FirstOrDefaultAsync(o =>
                    o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.Card_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _cardRepository.FirstOrDefaultAsync(o =>
                o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _cardRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCardsToExcel(GetAllCardInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };

            var objQuery = CardQuery(queryInput);

            var cards = await objQuery
                .OrderBy(input.Sorting ?? "id asc")
                .ToListAsync();

            var cardDtos = ObjectMapper.Map<List<CardDto>>(cards);

            return await _cardExcelExporter.ExportToFile(cardDtos);
        }
    }
}