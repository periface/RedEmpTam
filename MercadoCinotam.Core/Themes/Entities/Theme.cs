using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Themes.Entities
{
    public class Theme : FullAuditedEntity
    {
        public string ThemeUniqueName { get; set; }
        public string ThemeName { get; set; }
        public string ThemeDescription { get; set; }
    }
}
