using Abp.Configuration;
using Helpers.TenancyHelpers;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.ThemeService.Dtos;
using MercadoCinotam.ThemeService.Helpers;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MercadoCinotam.ThemeService.Client
{
    public class ThemeClientService : MercadoCinotamAppServiceBase, IThemeClientService
    {
        private readonly ISettingStore _settingStore;
        const string ServerPath = "/Content/HtmlContents/Tenants/{0}/{1}/";
        private const string ThemeHeaderContent = "/Views/Themes/{0}/Assets/DefaultContent/Header.txt";
        private const string ThemeBodyContent = "/Views/Themes/{0}/Assets/DefaultContent/Body.txt";
        const string FileNameHeader = "Header.txt";
        const string FileNameBody = "Body.txt";
        private readonly HttpServerUtility _server;
        public ThemeClientService(ISettingStore settingStore)
        {
            _settingStore = settingStore;
            _server = HttpContext.Current.Server;
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
                return string.Empty;
            }
        }
        public async Task<ThemeContentOutput> GetThemeContentFor(string selector)
        {
            var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);

            var tenantFolder = string.Format(ServerPath, TenantId, currentTheme.Value);
            var serverFileBodyRoute = GetServerFileRoute(tenantFolder, FileNameBody);
            var serverFileHeaderRoute = GetServerFileRoute(tenantFolder, FileNameHeader);
            try
            {
                var result = FileHelpers.GetThemeResultsBySelector(selector, serverFileBodyRoute, serverFileHeaderRoute);
                return result;
            }
            catch (Exception)
            {
                var resolveThemeHeaderPath = string.Format(ThemeHeaderContent, currentTheme.Value);
                var resolveThemeBodyPath = string.Format(ThemeBodyContent, currentTheme.Value);
                var defaultContentHeader = _server.MapPath(resolveThemeHeaderPath);
                var defaultContentBody = _server.MapPath(resolveThemeBodyPath);
                var result = FileHelpers.GetThemeResultsBySelector(selector, defaultContentBody, defaultContentHeader);
                return result;
            }
        }



        private string GetServerFileRoute(string tenantFolder, string fileNameBody)
        {
            return _server.MapPath(tenantFolder + fileNameBody);
        }
    }
}
