using Abp.Domain.Repositories;
using MercadoCinotam.Themes.Entities;
using MercadoCinotam.Themes.Manager;

namespace MercadoCinotam.Themes
{
    public class ThemeProvider : ThemeManager
    {
        public ThemeProvider(
            IRepository<Theme> themeRepository,
            IRepository<ThemePreview> themePreviewRepository)
            : base(themeRepository, themePreviewRepository)
        {
        }
    }
}
