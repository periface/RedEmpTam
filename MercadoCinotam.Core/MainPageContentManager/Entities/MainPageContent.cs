using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.MainPageContentManager.Entities
{
    public class MainPageContent : FullAuditedEntity, IMustHaveTenant
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsStatic { get; set; }
        public string ThemeReferenceId { get; set; }
        public string ThemeReferenceName { get; set; }
        public int TenantId { get; set; }
    }
}
