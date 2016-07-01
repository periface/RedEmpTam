using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MercadoCinotam.MainPageContentManager.Entities;

namespace MercadoCinotam.MainPageContentManager.Manager
{
    public class MainPageContentManager : DomainService, IMainPageContentManager
    {
        private readonly IRepository<MainPageContent> _mainPageContentRepository;

        public MainPageContentManager(IRepository<MainPageContent> mainPageContentRepository)
        {
            _mainPageContentRepository = mainPageContentRepository;
        }

        public int AddContent(MainPageContent content)
        {
            var id = _mainPageContentRepository.InsertOrUpdateAndGetId(content);
            CurrentUnitOfWork.SaveChanges();
            return id;
        }

        public MainPageContent GetContent(int id)
        {
            return _mainPageContentRepository.FirstOrDefault(a => a.Id == id);
        }
    }
}
