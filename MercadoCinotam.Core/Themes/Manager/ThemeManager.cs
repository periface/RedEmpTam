using Helpers.Helpers.HelperModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoCinotam.Themes.Manager
{
    public class ThemeManager : IThemeManager
    {

        public IEnumerable<ThemeInfo> GetThemes()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<ThemeInfo> GetTheme(string activeThemeName, dynamic server)
        {
            throw new System.NotImplementedException();
        }
    }
}
