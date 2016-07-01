using Abp.AutoMapper;
using Abp.UI;
using MercadoCinotam.MainPageContent.Dtos;
using MercadoCinotam.MainPageContentManager.Manager;

namespace MercadoCinotam.MainPageContent.Admin
{
    public class MainContentAdminService : IMainContentAdminService
    {
        public readonly IMainPageContentManager _contentManager;

        public MainContentAdminService(IMainPageContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public int AddEditContent(ContentInput input)
        {
            if (input.Id != 0)
            {
                var content = _contentManager.GetContent(input.Id);
                if (content == null) throw new UserFriendlyException();
                var edited = input.MapTo(content);
                return _contentManager.AddContent(edited);
            }
            var newContent = input.MapTo<MainPageContentManager.Entities.MainPageContent>();
            return _contentManager.AddContent(newContent);
        }

        public ContentInput GetContentForEdit(int? id)
        {
            if (!id.HasValue) return new ContentInput();
            var content = _contentManager.GetContent(id.Value);
            return content != null ? content.MapTo<ContentInput>() : new ContentInput();
        }
    }
}
