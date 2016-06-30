using Abp.Domain.Entities;

namespace MercadoCinotam.Themes.Entities
{
    public class ThemePreview : Entity
    {
        public string Image { get; set; }
        public Theme Theme { get; set; }
    }
}