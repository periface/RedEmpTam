using Abp.Domain.Services;
using System.Web;

namespace FileSaver.Manager
{
    public interface ISaverManager : IDomainService
    {
        string SaveAndGetVirtualPath(HttpPostedFileBase file, string folderPrefix);
    }
}
