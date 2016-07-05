using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Themes.Entities
{
    public class ThemeRequiredField : FullAuditedEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Theme Theme { get; set; }
        public string Type { get; set; }
    }
}
