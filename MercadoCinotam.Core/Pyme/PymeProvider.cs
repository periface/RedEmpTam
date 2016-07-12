using Abp.Domain.Repositories;
using MercadoCinotam.MainPageContentManager.Entities;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.Pyme.Manager;
using MercadoCinotam.Themes.Entities;

namespace MercadoCinotam.Pyme
{
    public class PymeProvider : PymeManager
    {
        public PymeProvider(
            IRepository<PymeInfo> pymeInfoRepository,
            IRepository<PymeContactInfo> pymeContactRepository,
            IRepository<MainPageContent> mainPageContentRepository,
            IRepository<ThemeRequiredField> themeFieldsRepository,
            IRepository<Theme> themeRepository)
            : base(pymeInfoRepository,
                  pymeContactRepository,
                  mainPageContentRepository,
                  themeFieldsRepository,
                  themeRepository)
        {
        }
    }
}
