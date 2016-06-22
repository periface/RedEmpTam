using Abp.Domain.Services;
using System.Web;

namespace ImageSaver.Manager
{
    public interface IImageManager : IDomainService
    {
        string SaveImage(int? width, int? height, HttpPostedFileBase image, string folder);
    }
}
