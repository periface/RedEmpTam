using Abp.Domain.Repositories;
using MercadoCinotam.MainPageContentManager.Entities;

namespace MercadoCinotam.MainPageContentManager
{
    public class MainPageContentProvider : Manager.MainPageContentManager
    {
        public MainPageContentProvider(IRepository<MainPageContent> mainPageContentRepository)
            : base(mainPageContentRepository)
        {
        }

    }
}
