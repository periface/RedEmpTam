using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace MercadoCinotam.Themes.Entities
{
    public class Theme : FullAuditedEntity
    {
        public string ThemeUniqueName { get; set; }
        public string ThemeName { get; set; }
        public string ThemeDescription { get; set; }
        public bool Released { get; set; }
        public virtual ICollection<ThemeRequiredField> ThemeRequiredFields { get; set; }
        public virtual ICollection<ThemePreview> ThemePreviews { get; set; }
    }
}
