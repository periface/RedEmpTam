using Abp.Domain.Services;
using Helpers.Helpers.HelperModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoCinotam.Themes.Manager
{
    public interface IThemeManager : IDomainService
    {
        IEnumerable<ThemeInfo> GetThemes();
        Task<ThemeInfo> GetTheme(string activeThemeName, dynamic server);
    }
}
