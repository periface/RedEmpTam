using Abp.Configuration;
using Helpers.Helpers;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes;
using MercadoCinotam.ThemeService.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MercadoCinotam.ThemeService.Admin
{
    public class ThemeAdminService : MercadoCinotamAppServiceBase, IThemeAdminService
    {
        private readonly ThemeProvider _themeManager;
        private readonly ISettingStore _settingStore;
        private const string ThemeFolder = "/Views/Themes/{0}";
        const string ServerPath = "/Content/HtmlContents/Tenants/{0}/{1}/";
        private const string ThemeHeaderContent = "/Views/Themes/{0}/Assets/DefaultContent/Header.txt";
        private const string ThemeBodyContent = "/Views/Themes/{0}/Assets/DefaultContent/Body.txt";
        const string FileNameHeader = "Header.txt";
        const string FileNameBody = "Body.txt";
        private readonly HttpServerUtility _server;
        public ThemeAdminService(ThemeProvider themeManager, ISettingStore settingStore)
        {
            _themeManager = themeManager;
            _settingStore = settingStore;
            _server = HttpContext.Current.Server;
        }

        public async Task<ThemeSelectorOutput> GetThemesForSelector()
        {
            var themes =
            await _themeManager.GetAllThemesFromFiles(_server);

            var activeThemeName = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
            var activeTheme = await _themeManager.GetTheme(activeThemeName.Value,_server);

            return new ThemeSelectorOutput()
            {
                Themes = themes.Select(a => new ThemeDto()
                {
                    Id = a.ThemeUniqueName,
                    Preview = GetPreviewsFromFolder(a.PreviewsFolder, a.ThemeUniqueName),
                    Released = a.InDevelopment,
                    ThemeDescription = a.ThemeDescription,
                    ThemeName = a.ThemeName,
                    ThemeUniqueName = a.ThemeUniqueName,

                }).ToList(),
                ActiveTheme = activeTheme
            };
        }

        private IEnumerable<ThemePreviewDto> GetPreviewsFromFolder(string previewsFolder, string themeUniqueName)
        {
            var folder = string.Format(ThemeFolder, themeUniqueName) + previewsFolder;
            var serverPath = _server.MapPath(folder);
            var files = Directory.GetFiles(serverPath);
            var listOfFiles = new List<ThemePreviewDto>();
            foreach (var file in files)
            {
                var virtualPath = folder + Path.GetFileName(file);
                listOfFiles.Add(new ThemePreviewDto()
                {
                    Image = virtualPath
                });
            }
            return listOfFiles;
        }

        public async Task<ThemeHtmlInput> GetThemeContentForEdit()
        {
            var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);

            var tenantFolder = string.Format(ServerPath, TenantId, currentTheme.Value);
            var serverFileBodyRoute = GetServerFileRoute(tenantFolder, FileNameBody);
            var serverFileHeaderRoute = GetServerFileRoute(tenantFolder, FileNameHeader);
            try
            {
                var fileHeader = FileHelpers.GetFileContentsAsString(serverFileHeaderRoute);
                var fileBody = FileHelpers.GetFileContentsAsString(serverFileBodyRoute);
                FileHelpers.CheckIfContentsAreEmpty(fileHeader, fileBody);
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
                var defaultContentHeader = _server.MapPath(resolveThemeHeaderPath);
                var defaultContentBody = _server.MapPath(resolveThemeBodyPath);
                var fileHeader = FileHelpers.GetFileContentsAsString(defaultContentHeader);
                var fileBody = FileHelpers.GetFileContentsAsString(defaultContentBody);
                return new ThemeHtmlInput()
                {
                    HtmlContentHeader = fileHeader,
                    HtmlContentBody = fileBody
                };
            }
        }
        public async Task CreateHtml(ThemeHtmlInput input)
        {
            var currentTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
            var resolvedThemePath = string.Format(ServerPath, TenantId, currentTheme.Value);
            var serverPath = _server.MapPath(resolvedThemePath);
            FileHelpers.CreateDirectory(serverPath);

            var fileHeaderDirectory = serverPath + FileNameHeader;
            var fileBodyDirectory = serverPath + FileNameBody;

            FileHelpers.ProcessFile(fileHeaderDirectory, input.HtmlContentHeader);
            FileHelpers.ProcessFile(fileBodyDirectory, input.HtmlContentBody);
        }



        public Task<int> RegisterThemeByFile()
        {
            throw new NotImplementedException();
        }

        private string GetServerFileRoute(string tenantFolder, string fileNameBody)
        {
            return _server.MapPath(tenantFolder + fileNameBody);
        }
    }
}
