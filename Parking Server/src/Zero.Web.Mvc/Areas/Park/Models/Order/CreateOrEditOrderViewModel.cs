using System.Collections.Generic;
using DPS.Park.Application.Shared.Dto.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zero.Web.Areas.Park.Models.Order
{
    public class CreateOrEditOrderViewModel
    {
        public CreateOrEditOrderDto Order { get; set; }

        public bool IsEditMode => Order.Id.HasValue;
        
        public List<SelectListItem> ListOrderStatus { get; set; }
    }
}