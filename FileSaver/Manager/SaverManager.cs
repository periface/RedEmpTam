using System;
using System.IO;
using System.Web;

namespace FileSaver.Manager
{
    public class SaverManager : ISaverManager
    {
        private readonly HttpServerUtility _server;

        public SaverManager()
        {
            _server = HttpContext.Current.Server;
        }

        public virtual string SaveAndGetVirtualPath(HttpPostedFileBase file, string folderPrefix)
        {
            var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var folder = string.Format(folderPrefix, "");
            var virtualPath = string.Format(folderPrefix, newFileName);
            var physicalPath = _server.MapPath(virtualPath);
            CreateFolder(folder);
            file.SaveAs(physicalPath);
            return virtualPath;
        }

        private void CreateFolder(string path)
        {
            var physicalFolder = _server.MapPath(path);
            if (!Directory.Exists(physicalFolder))
            {
                Directory.CreateDirectory(physicalFolder);
            }
        }
    }
}
