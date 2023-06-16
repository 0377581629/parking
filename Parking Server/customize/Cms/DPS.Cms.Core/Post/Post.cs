using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using DPS.Cms.Core.Shared;
using Zero.Customize.Base;

namespace DPS.Cms.Core.Post
{
    [Table("Cms_Post")]
    public class Post : SimpleFullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string About { get; set; }

        public string Summary { get; set; }
        
        public string Slug { get; set; }
        
        public string Url { get; set; }
        
        public string Image { get; set; }
    }
}