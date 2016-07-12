using MercadoCinotam.ThemeService.Dtos;
using System;
using System.IO;

namespace MercadoCinotam.ThemeService.Helpers
{
    public static class FileHelpers
    {
        public static void WriteFileInDirectory(string filePath, string contents)
        {
            using (var sw = File.CreateText(filePath))
            {
                sw.Write(contents);
                sw.Close();
                sw.Dispose();
            }
        }

        public static void CreateDirectory(string serverPath)
        {
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
        }

        public static void DeleteFile(string file)
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
        public static void CheckIfContentsAreEmpty(string fileHeader, string fileBody)
        {
            if (string.IsNullOrEmpty(fileHeader) || string.IsNullOrEmpty(fileBody))
            {
                throw new Exception();
            }
        }
        public static void CheckIfContentsAreEmpty(string fileContents)
        {
            if (string.IsNullOrEmpty(fileContents))
            {
                throw new Exception();
            }
        }
        public static string GetFileContentsAsString(string serverFile)
        {
            using (var reader = new StreamReader(serverFile))
            {
                var fileContents = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                return fileContents;
            }
        }
        public static void ProcessFile(string filePath, string content)
        {
            if (!File.Exists(filePath))
            {
                WriteFileInDirectory(filePath, content);
            }
            else
            {
                DeleteFile(filePath);
                WriteFileInDirectory(filePath, content);
            }
        }
        public static ThemeContentOutput GetThemeResultsBySelector(string selector, string serverFileBodyRoute, string serverFileHeaderRoute)
        {
            switch (selector)
            {
                case "Body":
                    var fileBody = FileHelpers.GetFileContentsAsString(serverFileBodyRoute);
                    FileHelpers.CheckIfContentsAreEmpty(fileBody);
                    return new ThemeContentOutput()
                    {
                        HtmlContent = fileBody
                    };
                case "Header":
                    var fileHeader = FileHelpers.GetFileContentsAsString(serverFileHeaderRoute);
                    FileHelpers.CheckIfContentsAreEmpty(fileHeader);
                    return new ThemeContentOutput()
                    {
                        HtmlContent = fileHeader
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
