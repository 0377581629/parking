using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Interface.Order;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Order
{
    public class OrderAppService : ZeroAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Core.Order.Order> _orderRepository;

        public OrderAppService(IRepository<Core.Order.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private IQueryable<OrderDto> OrderQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _orderRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Code.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new OrderDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Code = obj.Code,

                    CardId = obj.CardId,
                    CardCode = obj.Card.Code,
                    CardNumber = obj.Card.CardNumber,

                    Amount = obj.Amount,
                    Status = obj.Status,
                    VnpTransactionNo = obj.VnpTransactionNo,

                    CreationTime = obj.CreationTime
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllOrderInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetOrderForViewDto>> GetAll(GetAllOrderInput input)
        {
            var queryInput = new QueryInput()
            {
                Input = input
            };

            var objQuery = OrderQuery(queryInput);

            var pagedAndFilteredVehicleType = objQuery.OrderBy(input.Sorting ?? "creationTime desc").PageBy(input);

            var objs = from o in pagedAndFilteredVehicleType
                select new GetOrderForViewDto()
                {
                    Order = ObjectMapper.Map<OrderDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetOrderForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.Order_Edit)]
        public async Task<GetOrderForEditOutput> GetOrderForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var order = await OrderQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetOrderForEditOutput
            {
                Order = ObjectMapper.Map<CreateOrEditOrderDto>(order)
            };
            return output;
        }
        
        private async Task ValidateDataInput(CreateOrEditOrderDto input)
        {
            var res = await _orderRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code.Equals(input.Code))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task CreateOrEdit(CreateOrEditOrderDto input)
        {
            input.TenantId = AbpSession.TenantId;
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
        
        [AbpAuthorize(ParkPermissions.Order_Create)]
        protected virtual async Task Create(CreateOrEditOrderDto input)
        {
            var obj = ObjectMapper.Map<Core.Order.Order>(input);
            await _orderRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.Order_Edit)]
        protected virtual async Task Update(CreateOrEditOrderDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _orderRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        public async Task Delete(EntityDto input)
        {
            var obj = await _orderRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _orderRepository.DeleteAsync(input.Id);
        }
    }
}