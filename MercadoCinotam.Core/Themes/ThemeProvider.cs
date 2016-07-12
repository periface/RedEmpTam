using Helpers.Helpers;
using Helpers.Helpers.HelperModels;
using MercadoCinotam.Themes.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MercadoCinotam.Themes
{
    public class ThemeProvider : ThemeManager
    {
        private const string ThemesPath = "/Views/Themes/";
        private const string ActivationFileName = "Activator.json";

        public async Task<List<ThemeInfo>> GetAllThemesFromFiles(dynamic server)
        {
            var serverFolder = server.MapPath(ThemesPath);
            var allFolders = Directory.EnumerateDirectories(serverFolder);
            var result = new List<ThemeInfo>();
            foreach (var folder in allFolders)
            {
                try
                {

                    var activatorFileContents = GetActivationFile(folder);
                    var parsedData = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ThemeInfo>(activatorFileContents));
                    result.Add(parsedData);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return result;
        }

        private string GetActivationFile(string folder)
        {
            var content = FileHelpers.GetFileContentsAsString(folder + "/" + ActivationFileName);
            return content;
        }

        public override async Task<ThemeInfo> GetTheme(string activeThemeName, dynamic server)
        {
            var serverFolder = server.MapPath(ThemesPath);
            var allFolders = Directory.EnumerateDirectories(serverFolder);
            foreach (var folder in allFolders)
            {
                try
                {
                    var folderDir = folder.Remove(0, folder.LastIndexOf('\\') + 1);
                    if (folderDir == activeThemeName)
                    {
                        var activatorFileContents = GetActivationFile(folder);
                        var parsedData = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ThemeInfo>(activatorFileContents));
                        return parsedData;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return null;
        }
        public async Task<ThemeInfo> GetThemeByThemeName(string themeName, dynamic server)
        {
            var serverFolder = server.MapPath(ThemesPath);
            var allFolders = Directory.EnumerateDirectories(serverFolder);
            foreach (var folder in allFolders)
            {
                try
                {
                    var folderDir = folder.Remove(0, folder.LastIndexOf('\\') + 1);
                    if (folderDir == themeName)
                    {
                        var activatorFileContents = GetActivationFile(folder);
                        var parsedData = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ThemeInfo>(activatorFileContents));
                        return parsedData;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return null;
        }
    }
}
