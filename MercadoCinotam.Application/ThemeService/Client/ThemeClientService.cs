using Abp.Configuration;
using Helpers.TenancyHelpers;
using System;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Client
{
    public class ThemeClientService : MercadoCinotamAppServiceBase, IThemeClientService
    {
        private readonly ISettingStore _settingStore;

        public ThemeClientService(ISettingStore settingStore)
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
