using MercadoCinotam.MainPageContentManager.Entities;

namespace MercadoCinotam.MainPageContentManager.Manager
{
    public interface IMainPageContentManager
    {
        int AddContent(MainPageContent content);
        MainPageContent GetContent(int id);
    }
}
