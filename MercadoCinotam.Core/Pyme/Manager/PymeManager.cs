using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Helpers.TenancyHelpers;
using MercadoCinotam.MainPageContentManager.Entities;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.Themes.Entities;
using System;
using System.Linq;

namespace MercadoCinotam.Pyme.Manager
{
    public class PymeManager : DomainService, IPymeManager
    {
        private readonly IRepository<PymeInfo> _pymeInfoRepository;
        private readonly IRepository<PymeContactInfo> _pymeContactRepository;
        private readonly IRepository<MainPageContent> _mainPageContentRepository;
        private readonly IRepository<ThemeRequiredField> _themeFieldsRepository;
        private readonly IRepository<Theme> _themeRepository;
        public PymeManager(IRepository<PymeInfo> pymeInfoRepository, IRepository<PymeContactInfo> pymeContactRepository, IRepository<MainPageContent> mainPageContentRepository, IRepository<ThemeRequiredField> themeFieldsRepository, IRepository<Theme> themeRepository)
        {
            _pymeInfoRepository = pymeInfoRepository;
            _pymeContactRepository = pymeContactRepository;
            _mainPageContentRepository = mainPageContentRepository;
            _themeFieldsRepository = themeFieldsRepository;
            _themeRepository = themeRepository;
        }

        public int AddInfo(PymeInfo info)
        {
            if (TenantHelper.TenantId != null) info.TenantId = (int)TenantHelper.TenantId;
            var id = _pymeInfoRepository.InsertOrUpdateAndGetId(info);
            CurrentUnitOfWork.SaveChanges();
            return id;
        }

        public int AddOwner(PymeOwner owner)
        {
            throw new NotImplementedException();
        }

        public int AddContactInfo(PymeContactInfo contactInfo)
        {
            var id = _pymeContactRepository.InsertOrUpdateAndGetId(contactInfo);
            CurrentUnitOfWork.SaveChanges();
            return id;
        }

        public PymeInfo GetInfo(int tenantId)
        {
            var info = _pymeInfoRepository.FirstOrDefault(a => a.TenantId == tenantId);
            return info;
        }

        public PymeContactInfo GetContactInfo(int tenantId)
        {
            var contactInfo = _pymeContactRepository.FirstOrDefault(a => a.TenantId == tenantId);
            return contactInfo;
        }

        public void SetMainPageContent(int themeId, bool keepData)
        {
            var theme = _themeRepository.FirstOrDefault(a => a.Id == themeId);
            if (keepData)
            {

                foreach (var themeRequiredField in _themeFieldsRepository.GetAllList(a => a.Theme.Id == themeId))
                {
                    //Checks if exist in the current content config
                    var field = _mainPageContentRepository.FirstOrDefault(a => a.Key == themeRequiredField.Key);

                    if (field == null)
                    {
                        _mainPageContentRepository.InsertOrUpdateAndGetId(new MainPageContent()
                        {
                            Value = themeRequiredField.Value,
                            Key = themeRequiredField.Key,
                            ThemeReferenceId = themeId,
                            ThemeReferenceName = theme.ThemeName,
                            IsStatic = true
                        });
                    }
                }
            }
            else
            {
                foreach (var themeRequiredField in _themeFieldsRepository.GetAllList(a => a.Theme.Id == themeId))
                {
                    //Checks if exist in the current content config
                    var field = _mainPageContentRepository.FirstOrDefault(a => a.Key == themeRequiredField.Key);

                    if (field == null)
                    {
                        _mainPageContentRepository.InsertOrUpdateAndGetId(new MainPageContent()
                        {
                            Value = themeRequiredField.Value,
                            Key = themeRequiredField.Key,
                            ThemeReferenceId = themeId,
                            ThemeReferenceName = theme.ThemeName,
                            IsStatic = true
                        });
                    }
                    else
                    {
                        field.Value = themeRequiredField.Value;
                    }
                }
            }
            CurrentUnitOfWork.SaveChanges();
        }

        public IQueryable<MainPageContent> GetMainPageContentsQuery()
        {
            return _mainPageContentRepository.GetAll();
        }

        public object GetMainPageContent(string key, int tenantId)
        {
            var mainPageContent = _mainPageContentRepository.FirstOrDefault(a => a.TenantId == tenantId && a.Key == key);
            return mainPageContent == null ? "Valor no encontrado" : mainPageContent.Value;
        }

    }
}
