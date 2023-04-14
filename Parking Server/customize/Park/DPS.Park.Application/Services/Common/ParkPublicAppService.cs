using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Interface.Common;
using Microsoft.EntityFrameworkCore;
using Zero;

namespace DPS.Park.Application.Services.Common
{
    [AbpAllowAnonymous]
    public class ParkPublicAppService : ZeroAppServiceBase, IParkPublicAppService
    {
        #region Constructor

        private readonly IRepository<Core.Order.Order> _orderRepository;

        public ParkPublicAppService(IRepository<Core.Order.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion
        
        #region User

        [AbpAuthorize]
        public async Task<PagedResultDto<GetOrderForViewDto>> GetMyOrders(ParkPublicInput input)
        {
            var objQuery = from order in _orderRepository.GetAll()
                    .Where(e => e.CreatorUserId == AbpSession.UserId && !e.IsDeleted)
                    .WhereIf(input != null && !string.IsNullOrEmpty(input.Filter), o => o.Code.Contains(input.Filter))
                select new OrderDto
                {
                    TenantId = order.TenantId,
                    Id = order.Id,
                    Code = order.Code,

                    CardId = order.CardId,
                    CardCode = order.Card.Code,
                    CardNumber = order.Card.CardNumber,

                    Amount = order.Amount,
                    Status = order.Status,
                    VnpTransactionNo = order.VnpTransactionNo,

                    CreationTime = order.CreationTime
                };

            var pagedAndFilteredOrders = objQuery.OrderBy("creationTime desc").PageBy(input);


            var objs = from o in pagedAndFilteredOrders
                select new GetOrderForViewDto
                {
                    Order = ObjectMapper.Map<OrderDto>(o),
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetOrderForViewDto>(
                totalCount,
                res
            );
        }

        #endregion
        
    }
}