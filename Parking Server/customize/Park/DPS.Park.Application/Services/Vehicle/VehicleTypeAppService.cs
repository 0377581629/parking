using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;
using DPS.Park.Application.Shared.Interface.Vehicle;
using DPS.Park.Core.Vehicle;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Vehicle
{
    [AbpAuthorize(ParkPermissions.VehicleType)]
    public class VehicleTypeAppService: ZeroAppServiceBase, IVehicleTypeAppService
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleTypeAppService(IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        
        private IQueryable<VehicleTypeDto> VehicleTypeQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _vehicleTypeRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter), e => e.Name.Contains(input.Filter))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new VehicleTypeDto
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
            public GetAllVehicleTypeInput Input { get; init; }
            public int? Id { get; init; }
        }

        public async Task<PagedResultDto<GetVehicleTypeForViewDto>> GetAll(GetAllVehicleTypeInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };
            var objQuery = VehicleTypeQuery(queryInput);

            var pagedAndFilteredVehicleType = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredVehicleType
                select new GetVehicleTypeForViewDto
                {
                    VehicleType = ObjectMapper.Map<VehicleTypeDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetVehicleTypeForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(ParkPermissions.VehicleType_Edit)]
        public async Task<GetVehicleTypeForEditOutput> GetVehicleTypeForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var vehicleType = await VehicleTypeQuery(queryInput).FirstOrDefaultAsync();

            var output = new GetVehicleTypeForEditOutput
            {
                VehicleType = ObjectMapper.Map<CreateOrEditVehicleTypeDto>(vehicleType)
            };
            return output;
        }
        
        private async Task ValidateDataInput(CreateOrEditVehicleTypeDto input)
        {
            var res = await _vehicleTypeRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Name.Equals(input.Name))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("NameAlreadyExists"));
        }
        
        public async Task CreateOrEdit(CreateOrEditVehicleTypeDto input)
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

        [AbpAuthorize(ParkPermissions.VehicleType_Create)]
        protected virtual async Task Create(CreateOrEditVehicleTypeDto input)
        {
            var obj = ObjectMapper.Map<VehicleType>(input);
            await _vehicleTypeRepository.InsertAndGetIdAsync(obj);
        }

        [AbpAuthorize(ParkPermissions.VehicleType_Edit)]
        protected virtual async Task Update(CreateOrEditVehicleTypeDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _vehicleTypeRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
                if (obj == null) throw new UserFriendlyException(L("NotFound"));
                ObjectMapper.Map(input, obj);
            }
        }

        [AbpAuthorize(ParkPermissions.VehicleType_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _vehicleTypeRepository.FirstOrDefaultAsync(o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _vehicleTypeRepository.DeleteAsync(input.Id);
        }
    }
}