using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Dto.Card.CardType;
using DPS.Park.Application.Shared.Dto.Fare;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;
using DPS.Park.Application.Shared.Interface.Common;
using DPS.Park.Core.Card;
using DPS.Park.Core.Vehicle;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization.Roles;
using Zero.Authorization.Users;

namespace DPS.Park.Application.Services.Common
{
    [AbpAuthorize]
    public class ParkAppService : ZeroAppServiceBase, IParkAppService
    {
        #region Constructor

        private readonly RoleManager _roleManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;
        private readonly IRepository<CardType> _cardTypeRepository;
        private readonly IRepository<Core.Fare.Fare> _fareRepository;
        private readonly IRepository<Core.Card.Card> _cardRepository;

        public ParkAppService(RoleManager roleManager,
            IRepository<User, long> userRepository,
            IRepository<VehicleType> vehicleTypeRepository,
            IRepository<CardType> cardTypeRepository, IRepository<Core.Fare.Fare> fareRepository,
            IRepository<Core.Card.Card> cardRepository)
        {
            _roleManager = roleManager;
            _userRepository = userRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _cardTypeRepository = cardTypeRepository;
            _fareRepository = fareRepository;
            _cardRepository = cardRepository;
        }

        #endregion

        #region VehicleType

        private IQueryable<VehicleTypeDto> VehicleTypeDataQuery(GetAllVehicleTypeInput input = null)
        {
            var query = from o in _vehicleTypeRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter))
                select new VehicleTypeDto
                {
                    TenantId = o.TenantId,
                    Id = o.Id,
                    Name = o.Name,
                    Note = o.Note
                };
            return query;
        }

        public async Task<List<VehicleTypeDto>> GetAllVehicleTypes()
        {
            return await VehicleTypeDataQuery().ToListAsync();
        }

        public async Task<PagedResultDto<VehicleTypeDto>> GetPagedVehicleTypes(GetAllVehicleTypeInput input)
        {
            var objQuery = VehicleTypeDataQuery(input);
            var pagedAndFilteredObj = objQuery.OrderBy(input.Sorting ?? "name asc").PageBy(input);
            var totalCount = await objQuery.CountAsync();
            var res = await pagedAndFilteredObj.ToListAsync();

            return new PagedResultDto<VehicleTypeDto>(
                totalCount,
                res
            );
        }

        #endregion

        #region CardType

        private IQueryable<CardTypeDto> CardTypeDataQuery(GetAllCardTypeInput input = null)
        {
            var query = from o in _cardTypeRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter))
                select new CardTypeDto
                {
                    TenantId = o.TenantId,
                    Id = o.Id,
                    Name = o.Name,
                    Note = o.Note
                };
            return query;
        }

        public async Task<List<CardTypeDto>> GetAllCardTypes()
        {
            return await CardTypeDataQuery().ToListAsync();
        }

        public async Task<PagedResultDto<CardTypeDto>> GetPagedCardTypes(GetAllCardTypeInput input)
        {
            var objQuery = CardTypeDataQuery(input);
            var pagedAndFilteredObj = objQuery.OrderBy(input.Sorting ?? "name asc").PageBy(input);
            var totalCount = await objQuery.CountAsync();
            var res = await pagedAndFilteredObj.ToListAsync();

            return new PagedResultDto<CardTypeDto>(
                totalCount,
                res
            );
        }

        #endregion

        #region Fare

        private IQueryable<FareDto> FareQuery()
        {
            var query = from obj in _fareRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                select new FareDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,

                    CardTypeId = obj.CardTypeId,
                    CardTypeName = obj.CardType.Name,

                    VehicleTypeId = obj.VehicleTypeId,
                    VehicleTypeName = obj.VehicleType.Name,

                    Price = obj.Price,
                    DayOfWeekStart = obj.DayOfWeekStart,
                    DayOfWeekEnd = obj.DayOfWeekEnd
                };
            return query;
        }

        public async Task<List<FareDto>> GetAllFares()
        {
            return await FareQuery().ToListAsync();
        }

        #endregion

        #region Card

        private IQueryable<CardDto> CardDataQuery(GetAllCardInput input = null)
        {
            var query = from o in _cardRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.CardNumber.Contains(input.Filter))
                select new CardDto
                {
                    TenantId = o.TenantId,
                    Id = o.Id,
                    Code = o.Code,
                    CardNumber = o.CardNumber,
                    Balance = o.Balance,
                    Note = o.Note,
                    IsActive = o.IsActive,

                    CardTypeId = o.CardTypeId,
                    CardTypeName = o.CardType.Name,

                    VehicleTypeId = o.VehicleTypeId,
                    VehicleTypeName = o.VehicleType.Name,
                };

            return query;
        }

        public async Task<PagedResultDto<CardDto>> GetPagedCards(GetAllCardInput input)
        {
            var fares = _fareRepository.GetAll().Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId);

            var objQuery = CardDataQuery(input);
            var pagedAndFilteredObj = objQuery.OrderBy(input.Sorting ?? "cardNumber asc").PageBy(input);
            var totalCount = await objQuery.CountAsync();
            var res = await pagedAndFilteredObj.ToListAsync();

            foreach (var card in res)
            {
                card.Price = await fares
                    .Where(o => o.CardTypeId == card.CardTypeId && o.VehicleTypeId == card.VehicleTypeId)
                    .Select(o => o.Price).FirstOrDefaultAsync();
            }

            return new PagedResultDto<CardDto>(
                totalCount,
                res
            );
        }

        #endregion
    }
}