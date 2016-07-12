using Abp.AutoMapper;
using Abp.Configuration;
using Helpers.GenericTypes;
using Helpers.TenancyHelpers;
using ImageSaver;
using MercadoCinotam.Pyme;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.PymeInfo.Dtos;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes;
using MercadoCinotam.ThemeService.Client;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public class PymeAdminService : MercadoCinotamAppServiceBase, IPymeAdminService
    {
        private readonly PymeProvider _pymeManager;
        private readonly ImageProvider _imageManager;
        private readonly ThemeProvider _themeManager;
        private readonly ISettingStore _settingStore;
        private readonly IThemeClientService _themeClientService;
        private const string ImageFolder = "/Content/Images/Logos/Tentants/{0}/";
        public PymeAdminService(PymeProvider pymeManager, ImageProvider imageManager, ThemeProvider themeManager, ISettingStore settingStore, IThemeClientService themeClientService)
        {
            _pymeManager = pymeManager;
            _imageManager = imageManager;
            _themeManager = themeManager;
            _settingStore = settingStore;
            _themeClientService = themeClientService;
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

        public async Task<ReturnModel<MainPageContentDto>> GetMainPageContents(RequestModel request, bool onlyActiveTheme = false)
        {
            IQueryable<MainPageContentManager.Entities.MainPageContent> query;
            if (onlyActiveTheme)
            {
                var activeTheme = await _themeClientService.GetActiveThemeFromTenant();
                query = _pymeManager.GetMainPageContentsQuery().Where(a => a.ThemeReferenceName == activeTheme);

            }
            else
            {
                query = _pymeManager.GetMainPageContentsQuery();
            }
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
