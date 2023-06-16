using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using JetBrains.Annotations;
using Zero.Customize.Base;

namespace DPS.Cms.Core.Page
{
    [Table("Cms_Page_Layout")]
    public class PageLayout : SimpleFullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
    }
}