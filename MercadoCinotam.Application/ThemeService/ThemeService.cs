using Abp.Configuration;
using Helpers.TenancyHelpers;
using System;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService
{
    public class ThemeService : MercadoCinotamAppServiceBase, IThemeService
    {
        private readonly ISettingStore _settingStore;

        public ThemeService(ISettingStore settingStore)
        {
            _settingStore = settingStore;
        }

        public async Task<string> GetActiveThemeFromTenant()
        {
            try
            {
                var theme = await _settingStore.GetSettingOrNullAsync(TenantHelper.TenantId, null, "Theme");
                return theme.Value;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
