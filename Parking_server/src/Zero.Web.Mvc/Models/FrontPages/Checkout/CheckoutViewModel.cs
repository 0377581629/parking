using DPS.Park.Application.Shared.Dto.Order;

namespace Zero.Web.Models.FrontPages.Checkout
{
    public class CheckoutViewModel
    {
        public CreateOrEditOrderDto Order { get; set; }
        
        public string StudentCode { get; set; }
        
        public string StudentName { get; set; }
    }
}