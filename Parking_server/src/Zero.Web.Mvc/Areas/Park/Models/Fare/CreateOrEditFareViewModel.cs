using System.Collections.Generic;
using DPS.Park.Application.Shared.Dto.Fare;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zero.Web.Areas.Park.Models.Fare
{
    public class CreateOrEditFareViewModel
    {
        public CreateOrEditFareDto Fare { get; set; }

        public bool IsEditMode => Fare.Id.HasValue;
        
        public List<SelectListItem> ListFareType { get; set; }
    }
}