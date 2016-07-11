using Abp.AutoMapper;
using Abp.Configuration;
using Abp.UI;
using ImageSaver.Manager;
using MercadoCinotam.MainPageContent.Dtos;
using MercadoCinotam.MainPageContentManager.Manager;
using MercadoCinotam.StartupSettings;
using System.Threading.Tasks;

namespace MercadoCinotam.MainPageContent.Admin
{
    public class MainContentAdminService : MercadoCinotamAppServiceBase, IMainContentAdminService
    {
        private readonly IMainPageContentManager _contentManager;
        private readonly IImageManager _imageManager;
        private readonly ISettingStore _settingStore;
        private const string FileFolder = "/Content/HtmlContents/Tenants/{0}/ThemeFiles/{1}/";
        public MainContentAdminService(IMainPageContentManager contentManager, IImageManager imageManager, ISettingStore settingStore)
        {
            _contentManager = contentManager;
            _imageManager = imageManager;
            _settingStore = settingStore;
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
            newContent.ThemeReferenceId = 0;
            newContent.ThemeReferenceName = "Custom";
            newContent.IsStatic = false;
            return _contentManager.AddContent(newContent);
        }

        public ContentInput GetContentForEdit(int? id)
        {
            if (!id.HasValue) return new ContentInput();
            var content = _contentManager.GetContent(id.Value);
            return content != null ? content.MapTo<ContentInput>() : new ContentInput();
        }

        public async Task<int> AddEditContentWithFile(ContentInput model)
        {
            var activeTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
            if (model.Id == 0)
            {
                var mainPageContent = new MainPageContentManager.Entities.MainPageContent();
                var id = _contentManager.AddContent(mainPageContent);

                var resolvedFolder = string.Format(FileFolder, TenantId, activeTheme.Value);
                var image = _imageManager.SaveImage(null, null, model.File, resolvedFolder);
                mainPageContent.Value = image;
                return id;
            }
            else
            {
                var mainPageContent = _contentManager.GetContent(model.Id);
                var resolvedFolder = string.Format(FileFolder, TenantId, activeTheme.Value);
                mainPageContent.Key = model.Key;
                if (model.File.ContentLength <= 0) return mainPageContent.Id;
                var image = _imageManager.SaveImage(null, null, model.File, resolvedFolder);
                mainPageContent.Value = image;
                return mainPageContent.Id;
            }
        }
    }
}
