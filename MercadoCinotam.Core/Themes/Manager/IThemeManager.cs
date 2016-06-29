using Abp.Domain.Services;
using MercadoCinotam.Themes.Entities;
using System.Collections.Generic;

namespace MercadoCinotam.Themes.Manager
{
    public interface IThemeManager : IDomainService
    {
        int SaveTheme(Theme theme);
        IEnumerable<Theme> GetThemes();
        Theme GetTheme(int themeId);
        IEnumerable<ThemePreview> GetThemePreviews(int themeId);
    }
}
