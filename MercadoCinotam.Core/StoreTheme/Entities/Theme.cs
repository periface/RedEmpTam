using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.StoreTheme.Entities
{
    public class Theme : FullAuditedEntity, IMayHaveTenant
    {
        public string ThemeName { get; set; }
        public string FolderName { get; set; }
        public string ThemeDescription { get; set; }
        public int? TenantId { get; set; }
    }
}
