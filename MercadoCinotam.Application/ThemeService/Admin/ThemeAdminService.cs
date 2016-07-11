using Abp.AutoMapper;
using Abp.Configuration;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes.Manager;
using MercadoCinotam.ThemeService.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MercadoCinotam.ThemeService.Admin
{
    public class ThemeAdminService : MercadoCinotamAppServiceBase, IThemeAdminService
    {
        private readonly IThemeManager _themeManager;
        private readonly ISettingStore _settingStore;
        const string ServerPath = "/Content/HtmlContents/Tenants/{0}/{1}/";
        private const string ThemeHeaderContent = "/Views/Themes/{0}/Assets/DefaultContent/Header.txt";
        private const string ThemeBodyContent = "/Views/Themes/{0}/Assets/DefaultContent/Body.txt";
        const string FileNameHeader = "Header.txt";
        const string FileNameBody = "Body.txt";
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

        public async Task<ThemeHtmlInput> GetThemeContentForEdit()
        {
            var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);

            var tenantFolder = string.Format(ServerPath, TenantId, currentTheme.Value);
            var serverFileBodyRoute = HttpContext.Current.Server.MapPath(tenantFolder + FileNameBody);
            var serverFileHeaderRoute = HttpContext.Current.Server.MapPath(tenantFolder + FileNameHeader);
            try
            {

                string fileHeader;
                string fileBody;

                using (var reader = new StreamReader(serverFileHeaderRoute))
                {
                    fileHeader = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                using (var reader = new StreamReader(serverFileBodyRoute))
                {
                    fileBody = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                if (string.IsNullOrEmpty(fileHeader) || string.IsNullOrEmpty(fileHeader))
                {
                    throw new Exception();
                }

                return new ThemeHtmlInput()
                {
                    HtmlContentHeader = fileHeader,
                    HtmlContentBody = fileBody
                };
            }
            catch (Exception)
            {
                var resolveThemeHeaderPath = string.Format(ThemeHeaderContent, currentTheme.Value);
                var resolveThemeBodyPath = string.Format(ThemeBodyContent, currentTheme.Value);
                var defaultContentHeader = HttpContext.Current.Server.MapPath(resolveThemeHeaderPath);
                var defaultContentBody = HttpContext.Current.Server.MapPath(resolveThemeBodyPath);
                var fileHeader = new StreamReader(defaultContentHeader);
                var fileBody = new StreamReader(defaultContentBody);
                return new ThemeHtmlInput()
                {
                    HtmlContentHeader = fileHeader.ReadToEnd(),
                    HtmlContentBody = fileBody.ReadToEnd()
                };
            }
        }



        public async Task CreateHtml(ThemeHtmlInput input)
        {
            try
            {
                var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
                var resolvedThemePath = string.Format(ServerPath, TenantId, currentTheme.Value);
                var serverPath = HttpContext.Current.Server.MapPath(resolvedThemePath);
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                var fileHeaderDirectory = serverPath + FileNameHeader;
                var fileBodyDirectory = serverPath + FileNameBody;
                if (!File.Exists(fileHeaderDirectory))
                {
                    using (var sw = File.CreateText(fileHeaderDirectory))
                    {
                        sw.Write(input.HtmlContentHeader);
                        sw.Close();
                        sw.Dispose();
                    }
                }
                else
                {
                    DeleteFile(fileHeaderDirectory);
                    using (var sw = File.CreateText(fileHeaderDirectory))
                    {
                        sw.Write(input.HtmlContentHeader);
                        sw.Close();
                        sw.Dispose();
                    }
                }
                if (!File.Exists(fileBodyDirectory))
                {
                    using (var sw = File.CreateText(fileBodyDirectory))
                    {
                        sw.Write(input.HtmlContentBody);
                        sw.Close();
                        sw.Dispose();
                    }
                }
                else
                {
                    DeleteFile(fileBodyDirectory);
                    using (var sw = File.CreateText(fileBodyDirectory))
                    {
                        sw.Write(input.HtmlContentBody);
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
