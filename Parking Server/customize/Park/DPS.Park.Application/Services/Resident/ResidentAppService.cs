using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Resident;
using DPS.Park.Application.Shared.Dto.Resident.ResidentCard;
using DPS.Park.Application.Shared.Interface.Resident;
using DPS.Park.Core.Resident;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Resident
{
    [AbpAuthorize(ParkPermissions.Resident)]
    public class ResidentAppService : ZeroAppServiceBase, IResidentAppService
    {
        private readonly IRepository<Core.Resident.Resident> _residentRepository;
        private readonly IRepository<ResidentCard> _residentCardRepository;

        public ResidentAppService(IRepository<Core.Resident.Resident> residentRepository,
            IRepository<ResidentCard> residentCardRepository)
        {
            _residentRepository = residentRepository;
            _residentCardRepository = residentCardRepository;
        }

        private class QueryInput
        {
            public GetAllResidentInput Input { get; set; }

            public int? Id { get; set; }
        }

        private IQueryable<ResidentDto> ResidentQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _residentRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        o => o.ApartmentNumber.Contains(input.Filter) || o.OwnerFullName.Contains(input.Filter))
                    .WhereIf(id.HasValue, o => o.Id == id.Value)
                select new ResidentDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    ApartmentNumber = obj.ApartmentNumber,
                    OwnerFullName = obj.OwnerFullName,
                    OwnerEmail = obj.OwnerEmail,
                    OwnerPhone = obj.OwnerPhone,
                    IsPaid = obj.IsPaid,
                    IsActive = obj.IsActive
                };
            return query;
        }

        private IQueryable<ResidentCardDto> ResidentCardQuery(int residentId)
        {
            var query = from obj in _residentCardRepository.GetAll()
                    .Where(o => o.TenantId == AbpSession.TenantId && o.ResidentId == residentId)
                select new ResidentCardDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,

                    ResidentId = obj.ResidentId,
                    ApartmentNumber = obj.Resident.ApartmentNumber,
                    OwnerFullName = obj.Resident.OwnerFullName,
                    OwnerEmail = obj.Resident.OwnerEmail,
                    OwnerPhone = obj.Resident.OwnerPhone,

                    CardId = obj.CardId,
                    CardCode = obj.Card.Code,
                    CardNumber = obj.Card.CardNumber,

                    Note = obj.Note
                };
            return query;
        }

        public async Task<PagedResultDto<GetResidentForViewDto>> GetAll(GetAllResidentInput input)
        {
            var queryInput = new QueryInput()
            {
                Input = input
            };
            var objQuery = ResidentQuery(queryInput);

            var pagedAndFilteredResidents = objQuery.OrderBy(input.Sorting ?? "apartmentNumber asc").PageBy(input);

            var objs = from o in pagedAndFilteredResidents
                select new GetResidentForViewDto()
                {
                    Resident = ObjectMapper.Map<ResidentDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetResidentForViewDto>(
                totalCount,
                res
            );
        }

        public async Task<GetResidentForEditOutput> GetResidentForEdit(EntityDto input)
        {
            var queryInput = new QueryInput()
            {
                Id = input.Id
            };

            var query = ResidentQuery(queryInput);
            var detailQuery = ResidentCardQuery(input.Id);

            var resident = await query.FirstOrDefaultAsync();

            var output = new GetResidentForEditOutput()
            {
                Resident = ObjectMapper.Map<CreateOrEditResidentDto>(resident)
            };
            output.Resident.ResidentDetails = await detailQuery.ToListAsync();

            return output;
        }

        private async Task ValidateDataInput(CreateOrEditResidentDto input)
        {
            var res = await _residentRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId &&
                            o.ApartmentNumber == input.ApartmentNumber)
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("ApartmentNumberExists"));
        }

        public async Task CreateOrEdit(CreateOrEditResidentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            input.ResidentDetails ??= new List<ResidentCardDto>();

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

        [AbpAuthorize(ParkPermissions.Resident_Create)]
        protected virtual async Task Create(CreateOrEditResidentDto input)
        {
            EntityFrameworkManager.ContextFactory = _ => _residentRepository.GetDbContext();
            var obj = ObjectMapper.Map<Core.Resident.Resident>(input);
            await _residentRepository.InsertAndGetIdAsync(obj);

            var residentDetails = ObjectMapper.Map<List<ResidentCard>>(input.ResidentDetails);
            if (residentDetails.Any())
            {
                foreach (var detail in residentDetails)
                {
                    detail.TenantId = AbpSession.TenantId;
                    detail.ResidentId = obj.Id;
                }

                await _residentRepository.GetDbContext().BulkSynchronizeAsync(residentDetails,
                    options => { options.ColumnSynchronizeDeleteKeySubsetExpression = detail => detail.ResidentId; });
            }
            else
            {
                await _residentCardRepository.DeleteAsync(o => o.ResidentId == obj.Id);
            }
        }

        [AbpAuthorize(ParkPermissions.Resident_Edit)]
        protected virtual async Task Update(CreateOrEditResidentDto input)
        {
            EntityFrameworkManager.ContextFactory = _ => _residentRepository.GetDbContext();
            var obj = await _residentRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            ObjectMapper.Map(input, obj);

            var residentDetails = ObjectMapper.Map<List<ResidentCard>>(input.ResidentDetails);
            if (residentDetails.Any())
            {
                foreach (var detail in residentDetails)
                {
                    detail.TenantId = AbpSession.TenantId;
                    detail.ResidentId = obj.Id;
                }

                await _residentRepository.GetDbContext().BulkSynchronizeAsync(residentDetails,
                    options => { options.ColumnSynchronizeDeleteKeySubsetExpression = detail => detail.ResidentId; });
            }
            else
            {
                await _residentCardRepository.DeleteAsync(o => o.ResidentId == obj.Id);
            }
        }

        [AbpAuthorize(ParkPermissions.Resident_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _residentRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _residentRepository.DeleteAsync(input.Id);
            await _residentCardRepository.DeleteAsync(o => o.ResidentId == input.Id);
        }
    }
}