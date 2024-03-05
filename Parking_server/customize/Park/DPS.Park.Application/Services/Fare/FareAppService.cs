using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Fare;
using DPS.Park.Application.Shared.Interface.Fare;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Fare
{
    [AbpAuthorize(ParkPermissions.Fare)]
    public class FareAppService: ZeroAppServiceBase, IFareAppService
    {
        private readonly IRepository<Core.Fare.Fare> _fareRepository;

        public FareAppService(IRepository<Core.Fare.Fare> fareRepository)
        {
            _fareRepository = fareRepository;
        }
        
        private IQueryable<FareDto> FareQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _fareRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter), e => e.CardType.Name.Contains(input.Filter) || e.VehicleType.Name.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new FareDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    
                    CardTypeId = obj.CardTypeId,
                    CardTypeName = obj.CardType.Name,
                    
                    VehicleTypeId = obj.VehicleTypeId,
                    VehicleTypeName = obj.VehicleType.Name,
                    
                    Price = obj.Price,
                    Type = obj.Type
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllFareInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetFareForViewDto>> GetAll(GetAllFareInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };
            var objQuery = FareQuery(queryInput);

            var pagedAndFilteredFare = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredFare
                select new GetFareForViewDto
                {
                    Fare = ObjectMapper.Map<FareDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetFareForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.Fare_Edit)]
        public async Task<GetFareForEditOutput> GetFareForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var fare = await FareQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetFareForEditOutput
            {
                Fare = ObjectMapper.Map<CreateOrEditFareDto>(fare)
            };
            return output;
        }
        
        public async Task CreateOrEdit(CreateOrEditFareDto input)
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

        [AbpAuthorize(ParkPermissions.Fare_Create)]
        protected virtual async Task Create(CreateOrEditFareDto input)
        {
            var obj = ObjectMapper.Map<Core.Fare.Fare>(input);
            await _fareRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.Fare_Edit)]
        protected virtual async Task Update(CreateOrEditFareDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _fareRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.Fare_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _fareRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _fareRepository.DeleteAsync(input.Id);
        }
    }
}