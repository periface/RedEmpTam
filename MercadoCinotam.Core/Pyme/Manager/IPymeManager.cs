using Abp.Domain.Services;
using MercadoCinotam.MainPageContentManager.Entities;
using MercadoCinotam.Pyme.Entities;
using System.Linq;

namespace MercadoCinotam.Pyme.Manager
{
    public interface IPymeManager : IDomainService
    {
        int AddInfo(PymeInfo info);
        int AddOwner(PymeOwner owner);
        int AddContactInfo(PymeContactInfo contactInfo);

        PymeInfo GetInfo(int tenantId);
        PymeContactInfo GetContactInfo(int tenantId);
        void SetMainPageContent(int themeId, bool keepData);
        IQueryable<MainPageContent> GetMainPageContentsQuery();
        object GetMainPageContent(string key, int tenantId);
    }
}
