using MercadoCinotam.Themes.Entities;
using System;
using System.Collections.Generic;
using Theme = MercadoCinotam.StoreTheme.Entities.Theme;

namespace MercadoCinotam.Themes.Manager
{
    public class ThemeManager : IThemeManager
    {
        public int SaveTheme(Theme theme)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Theme> GetThemes()
        {
            throw new NotImplementedException();
        }

        public Theme GetTheme(int themeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThemePreview> GetThemePreviews(int themeId)
        {
            throw new NotImplementedException();
        }
    }
}
