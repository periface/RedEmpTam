using Abp.AutoMapper;
using Abp.Configuration;
using Abp.UI;
using ImageSaver;
using MercadoCinotam.MainPageContent.Dtos;
using MercadoCinotam.MainPageContentManager;
using MercadoCinotam.StartupSettings;
using System.Threading.Tasks;

namespace MercadoCinotam.MainPageContent.Admin
{
    public class MainContentAdminService : MercadoCinotamAppServiceBase, IMainContentAdminService
    {
        private readonly MainPageContentProvider _mainPageContentProvider;
        private readonly ImageProvider _imageProvider;
        private readonly ISettingStore _settingStore;
        private const string FileFolder = "/Content/HtmlContents/Tenants/{0}/ThemeFiles/{1}/";
        public MainContentAdminService(ISettingStore settingStore, MainPageContentProvider mainPageContentProvider, ImageProvider imageProvider)
        {
            _settingStore = settingStore;
            _mainPageContentProvider = mainPageContentProvider;
            _imageProvider = imageProvider;
        }

        public int AddEditContent(ContentInput input)
        {
            if (input.Id != 0)
            {
                var content = _mainPageContentProvider.GetContent(input.Id);
                if (content == null) throw new UserFriendlyException();
                var edited = input.MapTo(content);
                return _mainPageContentProvider.AddContent(edited);
            }
            var newContent = input.MapTo<MainPageContentManager.Entities.MainPageContent>();
            newContent.ThemeReferenceId = "Custom";
            newContent.ThemeReferenceName = "Custom";
            newContent.IsStatic = false;
            return _mainPageContentProvider.AddContent(newContent);
        }

        public ContentInput GetContentForEdit(int? id)
        {
            if (!id.HasValue) return new ContentInput();
            var content = _mainPageContentProvider.GetContent(id.Value);
            return content != null ? content.MapTo<ContentInput>() : new ContentInput();
        }

        public async Task<int> AddEditContentWithFile(ContentInput model)
        {
            var activeTheme = await _settingStore.GetSettingOrNullAsync(TenantId, null, ConfigConst.Theme);
            if (model.Id == 0)
            {
                var mainPageContent = new MainPageContentManager.Entities.MainPageContent();
                var id = _mainPageContentProvider.AddContent(mainPageContent);

                var resolvedFolder = string.Format(FileFolder, TenantId, activeTheme.Value);
                var image = _imageProvider.SaveImage(null, null, model.File, resolvedFolder);
                mainPageContent.Value = image;
                return id;
            }
            else
            {
                var mainPageContent = _mainPageContentProvider.GetContent(model.Id);
                var resolvedFolder = string.Format(FileFolder, TenantId, activeTheme.Value);
                mainPageContent.Key = model.Key;
                if (model.File.ContentLength <= 0) return mainPageContent.Id;
                var image = _imageProvider.SaveImage(null, null, model.File, resolvedFolder);
                mainPageContent.Value = image;
                return mainPageContent.Id;
            }
        }
    }
}
