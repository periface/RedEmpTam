using Abp.Configuration;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService
{
    public class ThemeService : IThemeService
    {
        private readonly ISettingManager _settingManager;

        public ThemeService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<string> GetActiveThemeFromTenant()
        {
            var theme = await _settingManager.GetSettingValueAsync("Theme");
            return theme;
        }
    }
}
