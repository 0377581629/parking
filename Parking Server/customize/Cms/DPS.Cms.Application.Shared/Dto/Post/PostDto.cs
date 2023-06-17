using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using DPS.Cms.Core.Shared;
using Zero;
using Zero.Customize.Base;
using Zero.Customize.Dto.Base;

namespace DPS.Cms.Application.Shared.Dto.Post
{
    [AutoMapFrom(typeof(Core.Post.Post))]
    public class PostDto : SimpleFullAuditedEntityDto, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string About { get; set; }
        public string Summary { get; set; }
        
        public string Slug { get; set; }
        
        public string Url { get; set; }
        
        public string Image { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}