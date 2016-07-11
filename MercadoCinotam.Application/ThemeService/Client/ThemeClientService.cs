using Abp.Configuration;
using Helpers.TenancyHelpers;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.ThemeService.Dtos;
using System;
using System.IO;
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
        public async Task<ThemeContentOutput> GetThemeContentFor(string selector)
        {
            var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);

            var tenantFolder = string.Format(ServerPath, TenantId, currentTheme.Value);
            var serverFileBodyRoute = HttpContext.Current.Server.MapPath(tenantFolder + FileNameBody);
            var serverFileHeaderRoute = HttpContext.Current.Server.MapPath(tenantFolder + FileNameHeader);
            try
            {
                string content;
                switch (selector)
                {
                    case "Body":
                        using (var reader = new StreamReader(serverFileBodyRoute))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        if (string.IsNullOrEmpty(content))
                        {
                            throw new Exception();
                        }
                        break;
                    case "Header":
                        using (var reader = new StreamReader(serverFileHeaderRoute))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        if (string.IsNullOrEmpty(content))
                        {
                            throw new Exception();
                        }
                        break;
                    default:
                        throw new Exception();
                }
                return new ThemeContentOutput()
                {
                    HtmlContent = content
                };
            }
            catch (Exception)
            {
                var resolveThemeHeaderPath = string.Format(ThemeHeaderContent, currentTheme.Value);
                var resolveThemeBodyPath = string.Format(ThemeBodyContent, currentTheme.Value);
                var defaultContentHeader = HttpContext.Current.Server.MapPath(resolveThemeHeaderPath);
                var defaultContentBody = HttpContext.Current.Server.MapPath(resolveThemeBodyPath);
                string content;
                switch (selector)
                {
                    case "Body":
                        using (var reader = new StreamReader(defaultContentBody))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        if (string.IsNullOrEmpty(content))
                        {
                            throw new Exception();
                        }
                        break;
                    case "Header":
                        using (var reader = new StreamReader(defaultContentHeader))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        if (string.IsNullOrEmpty(content))
                        {
                            throw new Exception();
                        }
                        break;
                    default:
                        throw new Exception();
                }
                return new ThemeContentOutput()
                {
                    HtmlContent = content
                };
            }
        }
    }
}
