using System.Collections.Generic;
using DPS.Park.Application.Shared.Dto.History;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zero.Web.Areas.Park.Models.History
{
    public class CreateOrEditHistoryViewModel
    {
        public CreateOrEditHistoryDto History { get; set; }

        public bool IsEditMode => History.Id.HasValue;
        
        public List<SelectListItem> ListHistoryType { get; set; }
    }
}