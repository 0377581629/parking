using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.History;
using DPS.Park.Application.Shared.Interface.History;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.History
{
    [AbpAuthorize(ParkPermissions.History)]
    public class HistoryAppService : ZeroAppServiceBase, IHistoryAppService
    {
        private readonly IRepository<Core.History.History> _historyRepository;

        public HistoryAppService(IRepository<Core.History.History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        private IQueryable<HistoryDto> HistoryQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _historyRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Card.CardNumber.Contains(input.Filter) || e.LicensePlate.Contains(input.Filter))
                    .WhereIf(input is {FromDate: { }}, o => input.FromDate.Value <= o.Time)
                    .WhereIf(input is {ToDate: { }}, o => input.ToDate.Value >= o.Time)
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                    .Include(o => o.Card.CardType)
                    .Include(o => o.Card.VehicleType)
                select new HistoryDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    CardId = obj.CardId,
                    CardCode = obj.Card != null ? obj.Card.Code : "",
                    CardNumber = obj.Card != null ? obj.Card.CardNumber : "",
                    LicensePlate = obj.LicensePlate,
                    Price = obj.Price,
                    Time = obj.Time,
                    Type = obj.Type,
                    Photo = obj.Photo,
                    CardTypeName = obj.Card.CardType.Name,
                    VehicleTypeName = obj.Card.VehicleType.Name
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllHistoryInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetHistoryForViewDto>> GetAll(GetAllHistoryInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };
            var objQuery = HistoryQuery(queryInput);

            var pagedAndFilteredHistory = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredHistory
                select new GetHistoryForViewDto
                {
                    History = ObjectMapper.Map<HistoryDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetHistoryForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.History_Edit)]
        public async Task<GetHistoryForEditOutput> GetHistoryForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var history = await HistoryQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetHistoryForEditOutput
            {
                History = ObjectMapper.Map<CreateOrEditHistoryDto>(history)
            };
            return output;
        }

        public async Task CreateOrEdit(CreateOrEditHistoryDto input)
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

        [AbpAuthorize(ParkPermissions.History_Create)]
        protected virtual async Task Create(CreateOrEditHistoryDto input)
        {
            var obj = ObjectMapper.Map<Core.History.History>(input);
            await _historyRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.History_Edit)]
        protected virtual async Task Update(CreateOrEditHistoryDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _historyRepository.FirstOrDefaultAsync(o =>
                    o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.History_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _historyRepository.FirstOrDefaultAsync(o =>
                o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _historyRepository.DeleteAsync(input.Id);
        }
    }
}