using Abp.UI;
using FileSaver.Manager;
using ImageResizer;
using System;
using System.Collections.Specialized;
using System.Web;

namespace ImageSaver.Manager
{
    public class ImageManager : IImageManager
    {
        private readonly ISaverManager _saverManager;

        public ImageManager(ISaverManager saverManager)
        {
            _saverManager = saverManager;
        }

        public string SaveImage(int? width, int? height, HttpPostedFileBase image, string folder)
        {
            var guid = Guid.NewGuid();
            if (!width.HasValue && !height.HasValue)
            {
                _saverManager.SaveAndGetVirtualPath(image, folder + "{0}");
            }
            if (image.ContentLength <= 0)
            {
                throw new UserFriendlyException("No se encontro imagen");
            }
            var inst = new NameValueCollection { { "width", width.ToString() }, { "height", height.ToString() } };
            var i = new ImageJob(image, "~/" + folder + "/" + guid + ".<ext>", new Instructions(inst))
            {
                CreateParentDirectory = true
            };

            i.Build();
            return folder + guid + "." + i.ResultFileExtension;
        }
    }
}
