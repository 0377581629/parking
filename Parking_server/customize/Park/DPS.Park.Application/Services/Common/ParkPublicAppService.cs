using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Contact.UserContact;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Dto.Student;
using DPS.Park.Application.Shared.Interface.Common;
using DPS.Park.Core.Contact;
using DPS.Park.Core.Shared;
using DPS.Park.Core.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zero;
using Zero.Configuration;
using Zero.Customize;

namespace DPS.Park.Application.Services.Common
{
    [AbpAllowAnonymous]
    public class ParkPublicAppService : ZeroAppServiceBase, IParkPublicAppService
    {
        #region Constructor

        private readonly IRepository<Core.Order.Order> _orderRepository;
        private readonly IRepository<Core.Student.Student> _studentRepository;
        private readonly IAppConfigurationAccessor _configurationAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserContact> _userContactRepository;
        private readonly IRepository<Core.Card.Card> _cardRepository;
        private readonly IRepository<StudentCard> _studentCardRepository;

        public ParkPublicAppService(IRepository<Core.Order.Order> orderRepository,
            IRepository<Core.Student.Student> studentRepository, IAppConfigurationAccessor configurationAccessor,
            IHttpContextAccessor httpContextAccessor, IRepository<UserContact> userContactRepository,
            IRepository<Core.Card.Card> cardRepository, IRepository<StudentCard> studentCardRepository)
        {
            _orderRepository = orderRepository;
            _studentRepository = studentRepository;
            _configurationAccessor = configurationAccessor;
            _httpContextAccessor = httpContextAccessor;
            _userContactRepository = userContactRepository;
            _cardRepository = cardRepository;
            _studentCardRepository = studentCardRepository;
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

        #region Order

        public async Task<int> CreateOrder(CreateOrEditOrderDto input)
        {
            input.TenantId = AbpSession.TenantId;
            input.Code = StringHelper.ShortIdentity();
            input.Status = (int) ParkEnums.OrderStatus.Waiting;

            var obj = ObjectMapper.Map<Core.Order.Order>(input);
            await _orderRepository.InsertAndGetIdAsync(obj);

            return obj.Id;
        }

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

        #region Checkout vnpay

        public async Task<string> UrlPayment(int typePayment, int orderId)
        {
            var urlPayment = "";
            var order = await _orderRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Id == orderId);
            //Get Config Info
            string vnp_Returnurl =
                _configurationAccessor.Configuration["Payment:VNPay:vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = _configurationAccessor.Configuration["Payment:VNPay:vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode =
                _configurationAccessor.Configuration[
                    "Payment:VNPay:vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = _configurationAccessor.Configuration["Payment:VNPay:vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var price = (long) order.Amount * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount",
                price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (typePayment == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (typePayment == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (typePayment == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreationTime.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef",
                order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }

        #endregion

        #region Contact

        private async Task ValidateUserContactDataInput(CreateOrEditUserContactDto input)
        {
            var res = await _userContactRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code.Equals(input.Code))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task GetUserContact(CreateOrEditUserContactDto input)
        {
            input.Code = StringHelper.ShortIdentity();
            input.TenantId = AbpSession.TenantId;
            await ValidateUserContactDataInput(input);

            if (input.Id == null)
            {
                await Create(input);
            }
        }

        protected virtual async Task Create(CreateOrEditUserContactDto input)
        {
            var obj = ObjectMapper.Map<UserContact>(input);
            await _userContactRepository.InsertAndGetIdAsync(obj);
        }

        #endregion

        #region Card

        public async Task<PagedResultDto<GetCardForViewDto>> GetMyCards(ParkPublicInput input)
        {
            var studentByCurrentUser = await _studentRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.UserId == AbpSession.UserId);
            var listCardIdOfStudent = await _studentCardRepository.GetAll()
                .Where(o => o.TenantId == AbpSession.TenantId && o.StudentId == studentByCurrentUser.Id)
                .Select(o => o.CardId).ToListAsync();

            var objQuery = from obj in _cardRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Code.Contains(input.Filter) || e.CardNumber.Contains(input.Filter) ||
                             e.Note.Contains(input.Filter))
                    .Where(o => listCardIdOfStudent.Contains(o.Id))
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
                    VehicleTypeName = obj.VehicleType.Name,
                    Balance = obj.Balance,
                    LicensePlate = obj.LicensePlate
                };

            var pagedAndFilteredOrders = objQuery.OrderBy("id asc").PageBy(input);


            var objs = from o in pagedAndFilteredOrders
                select new GetCardForViewDto
                {
                    Card = ObjectMapper.Map<CardDto>(o),
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetCardForViewDto>(
                totalCount,
                res
            );
        }

        #endregion
    }
}