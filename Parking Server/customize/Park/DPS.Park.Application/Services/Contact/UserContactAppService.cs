using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Contact.UserContact;
using DPS.Park.Application.Shared.Interface.Contact;
using DPS.Park.Core.Contact;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Contact
{
    [AbpAuthorize(ParkPermissions.UserContact)]
    public class UserContactAppService: ZeroAppServiceBase, IUserContactAppService
    {
        private readonly IRepository<UserContact> _userContactRepository;

        public UserContactAppService(
            IRepository<UserContact> userContactRepository)
        {
            _userContactRepository = userContactRepository;
        }

        private IQueryable<UserContactDto> UserContactQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _userContactRepository.GetAll()
                    .Where(o => !o.IsDeleted)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter) ||
                             e.Email.Contains(input.Filter) || e.Content.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new UserContactDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Code = obj.Code,
                    Name = obj.Name,
                    Email = obj.Email,
                    Phone = obj.Phone,
                    Title = obj.Title,
                    Content = obj.Content,
                    Note = obj.Note,
                    IsActive = obj.IsActive
                };
            return query;
        }

        private class QueryInput
        {
            public GetAllUserContactInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetUserContactForViewDto>> GetAll(GetAllUserContactInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var queryInput = new QueryInput
                {
                    Input = input
                };

                var objQuery = UserContactQuery(queryInput);
                var pagedAndFilteredUserContacts = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

                var objs = from o in pagedAndFilteredUserContacts
                    select new GetUserContactForViewDto
                    {
                        UserContact = ObjectMapper.Map<UserContactDto>(o)
                    };

                var totalCount = await objQuery.CountAsync();
                var res = await objs.ToListAsync();

                return new PagedResultDto<GetUserContactForViewDto>(
                    totalCount,
                    res
                );
            }
        }

        [AbpAuthorize(ParkPermissions.UserContact_Edit)]
        public async Task<GetUserContactForEditOutput> GetUserContactForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var query = UserContactQuery(queryInput);
            var userContact = await query.FirstOrDefaultAsync();

            var output = new GetUserContactForEditOutput
            {
                UserContact = ObjectMapper.Map<CreateOrEditUserContactDto>(userContact)
            };
            return output;
        }

        private async Task ValidateDataInput(CreateOrEditUserContactDto input)
        {
            var res = await _userContactRepository.GetAll()
                .Where(o => !o.IsDeleted && o.Code.Equals(input.Code))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task CreateOrEdit(CreateOrEditUserContactDto input)
        {
            input.Code = input.Code.Replace(" ", "");
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

        [AbpAuthorize(ParkPermissions.UserContact_Create)]
        protected virtual async Task Create(CreateOrEditUserContactDto input)
        {
            var obj = ObjectMapper.Map<UserContact>(input);
            await _userContactRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.UserContact_Edit)]
        protected virtual async Task Update(CreateOrEditUserContactDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _userContactRepository.FirstOrDefaultAsync(o => o.Id == input.Id);
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.UserContact_Delete)]
        public async Task Delete(EntityDto input)
        {
            var userContact = await _userContactRepository.FirstOrDefaultAsync(o => o.Id == input.Id);
            if (userContact == null) throw new UserFriendlyException(L("NotFound"));
            await _userContactRepository.DeleteAsync(input.Id);
        }
    }
}