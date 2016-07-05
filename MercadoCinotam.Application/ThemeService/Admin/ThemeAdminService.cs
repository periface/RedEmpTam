using Abp.AutoMapper;
using Abp.Configuration;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes.Manager;
using MercadoCinotam.ThemeService.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Admin
{
    public class ThemeAdminService : MercadoCinotamAppServiceBase, IThemeAdminService
    {
        private readonly IThemeManager _themeManager;
        private readonly ISettingStore _settingStore;
        public ThemeAdminService(IThemeManager themeManager, ISettingStore settingStore)
        {
            _themeManager = themeManager;
            _settingStore = settingStore;
        }

        public async Task<ThemeSelectorOutput> GetThemesForSelector()
        {
            var themes = _themeManager.GetThemes();

            var activeThemeName = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
            var activeTheme = _themeManager.GetTheme(activeThemeName.Value);
            var activeThemeDto = activeTheme.MapTo<ThemeDto>();
            return new ThemeSelectorOutput()
            {
                Themes = themes.Select(a => new ThemeDto()
                {
                    Id = a.Id,
                    Preview = _themeManager.GetThemePreviews(a.Id).Select(t => t.MapTo<ThemePreviewDto>()),
                    Released = a.Released,
                    ThemeDescription = a.ThemeDescription,
                    ThemeName = a.ThemeName,
                    ThemeUniqueName = a.ThemeUniqueName,

                }).ToList(),
                ActiveTheme = activeThemeDto
            };
        }
    }
}
