using Abp.AutoMapper;
using Abp.Configuration;
using Helpers.GenericTypes;
using Helpers.TenancyHelpers;
using ImageSaver.Manager;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.Pyme.Manager;
using MercadoCinotam.PymeInfo.Dtos;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes.Manager;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public class PymeAdminService : MercadoCinotamAppServiceBase, IPymeAdminService
    {
        private readonly IPymeManager _pymeManager;
        private readonly IImageManager _imageManager;
        private readonly IThemeManager _themeManager;
        private readonly ISettingStore _settingStore;
        private const string ImageFolder = "/Content/Images/Logos/Tentants/{0}/";
        public PymeAdminService(IPymeManager pymeManager, IImageManager imageManager, IThemeManager themeManager, ISettingStore settingStore)
        {
            _pymeManager = pymeManager;
            _imageManager = imageManager;
            _themeManager = themeManager;
            _settingStore = settingStore;
        }

        public int AddInfo(PymeInfoInput input)
        {
            Pyme.Entities.PymeInfo info;
            if (input.Id != 0)
            {
                var infoDb = _pymeManager.GetInfo(TenantId);
                info = input.MapTo(infoDb);
            }
            else
            {
                info = input.MapTo<Pyme.Entities.PymeInfo>();
            }
            if (input.Image.ContentLength > 0)
            {
                var formatedFolder = string.Format(ImageFolder, TenantHelper.TenantId);
                var image = _imageManager.SaveImage(null, null, input.Image, formatedFolder);
                info.PymeLogo = image;
            }
            var id = _pymeManager.AddInfo(info);
            return id;
        }

        public PymeInfoInput GetInfoForEdit()
        {
            var pymeInfo = _pymeManager.GetInfo(TenantId);
            return pymeInfo == null ? new PymeInfoInput() : pymeInfo.MapTo<PymeInfoInput>();
        }

        public int AddContactInfo(PymeContactInfoInput input)
        {
            PymeContactInfo contactInfo;
            if (input.Id != 0)
            {
                contactInfo = _pymeManager.GetContactInfo(TenantId);
                var info = input.MapTo(contactInfo);
                return _pymeManager.AddContactInfo(info);
            }
            contactInfo = input.MapTo<PymeContactInfo>();
            return _pymeManager.AddContactInfo(contactInfo);
        }

        public PymeContactInfoInput GetContactInfoForEdit()
        {
            var contactInfo = _pymeManager.GetContactInfo(TenantId);
            return contactInfo == null ? new PymeContactInfoInput() : contactInfo.MapTo<PymeContactInfoInput>();
        }

        public async Task SetTheme(SetThemeInput input)
        {
            var theme = _themeManager.GetTheme(input.ThemeId);
            await _settingStore.UpdateAsync(Settings.ThemeInitialConfig(TenantId, theme.ThemeUniqueName));

            _pymeManager.SetMainPageContent(theme.Id, input.KeepOldData);
        }

        public ReturnModel<MainPageContentDto> GetMainPageContents(RequestModel request)
        {
            var query = _pymeManager.GetMainPageContentsQuery();
            int totalCount;
            var filterByLength = GenerateModel(request, query, "Key", out totalCount);

            return new ReturnModel<MainPageContentDto>()
            {
                draw = request.draw,
                length = request.length,
                recordsTotal = totalCount,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = query.Count(),
                data = filterByLength.Select(a => a.MapTo<MainPageContentDto>()).ToArray(),
                recordsFiltered = filterByLength.Count
            };


        }

    }
}
