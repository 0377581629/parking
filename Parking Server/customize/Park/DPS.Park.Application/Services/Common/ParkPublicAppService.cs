using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Dto.Student;
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
        private readonly IRepository<Core.Student.Student> _studentRepository;

        public ParkPublicAppService(IRepository<Core.Order.Order> orderRepository,
            IRepository<Core.Student.Student> studentRepository)
        {
            _orderRepository = orderRepository;
            _studentRepository = studentRepository;
        }

        #endregion

        #region Order

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

        #region Student

        private IQueryable<StudentDto> StudentQuery(ParkPublicInput input)
        {
            var query = from obj in _studentRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        o => o.Code.Contains(input.Filter) || o.Name.Contains(input.Filter))
                    .WhereIf(input is {UserId: { }}, o => o.UserId == input.UserId.Value)
                    .WhereIf(input is {StudentId: { }}, o => o.Id == input.StudentId.Value)
                select new StudentDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Code = obj.Code,
                    Name = obj.Name,
                    Avatar = obj.Avatar,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Gender = obj.Gender,
                    DoB = obj.DoB,
                    IsActive = obj.IsActive,

                    UserId = obj.UserId,
                    UserName = obj.User.UserName,
                    UserEmail = obj.User.EmailAddress
                };
            return query;
        }

        public async Task<StudentDto> GetStudentByUserId(ParkPublicInput input)
        {
            var objQuery = StudentQuery(input);

            var res = await objQuery.FirstOrDefaultAsync();
            
            return res;
        }
        #endregion
    }
}