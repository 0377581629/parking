using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using DPS.Cms.Core.Shared;
using JetBrains.Annotations;
using Zero;
using Zero.Customize.Dto.Base;

namespace DPS.Cms.Application.Shared.Dto.Post
{
    public class CreateOrEditPostDto  : SimpleCreateOrEditEntityDto, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        
        public string About { get; set; }
        public string Summary { get; set; }
        
        public string Slug { get; set; }
        
        public string Url { get; set; }
        public string Image { get; set; }
    }
}