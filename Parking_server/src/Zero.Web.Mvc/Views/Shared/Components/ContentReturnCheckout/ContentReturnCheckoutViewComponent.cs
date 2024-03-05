using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Interface.Common;
using DPS.Park.Core.Card;
using DPS.Park.Core.Order;
using DPS.Park.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using Zero.Configuration;
using Zero.Web.Models.FrontPages.Checkout;

namespace Zero.Web.Views.Shared.Components.ContentReturnCheckout
{
    public class ContentReturnCheckoutViewComponent : ZeroViewComponent
    {
        private readonly IParkPublicAppService _parkPublicAppService;
        private readonly IAppConfigurationAccessor _configurationAccessor;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Card> _cardRepository;

        public ContentReturnCheckoutViewComponent(IParkPublicAppService parkPublicAppService,
            IAppConfigurationAccessor configurationAccessor, IRepository<Order> orderRepository,
            IRepository<Card> cardRepository)
        {
            _parkPublicAppService = parkPublicAppService;
            _configurationAccessor = configurationAccessor;
            _orderRepository = orderRepository;
            _cardRepository = cardRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var student = await _parkPublicAppService.GetStudentByUserId(new ParkPublicInput()
            {
                UserId = AbpSession.UserId
            });

            var model = new CheckoutViewModel()
            {
                Order = new CreateOrEditOrderDto(),
                StudentCode = student.Code,
                StudentName = student.Name
            };

            if (Request.Query.Count > 0)
            {
                var vnpHashSecret = _configurationAccessor.Configuration["Payment:VNPay:vnp_HashSecret"]; //Secret Key
                var vnpayData = Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (var queryData in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(queryData.Key) && queryData.Key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(queryData.Key, queryData.Value);
                    }
                }

                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                String TerminalID = Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.Query["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnpHashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = await
                            _orderRepository.FirstOrDefaultAsync(o =>
                                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.Status = (int) ParkEnums.OrderStatus.Success; //đã thanh toán
                            itemOrder.VnpTransactionNo = vnpayTranId;
                        }

                        var card = await _cardRepository.FirstOrDefaultAsync(o =>
                            !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.IsActive &&
                            o.Id == itemOrder.CardId);

                        card.Balance += (int) vnp_Amount;

                        await _orderRepository.UpdateAsync(itemOrder);
                        await _cardRepository.UpdateAsync(card);

                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                    }

                    ViewBag.VnpayTranId = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.CheckoutSuccess = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    ViewBag.Bank = "Ngân hàng thanh toán:" + bankCode;
                }
            }

            return View(model);
        }
    }
}