﻿using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Helpers.TenancyHelpers;
using MercadoCinotam.MainPageContentManager.Entities;
using MercadoCinotam.Pyme.Entities;
using System;
using System.Linq;

namespace MercadoCinotam.Pyme.Manager
{
    public class PymeManager : DomainService, IPymeManager
    {
        private readonly IRepository<PymeInfo> _pymeInfoRepository;
        private readonly IRepository<PymeContactInfo> _pymeContactRepository;
        private readonly IRepository<MainPageContent> _mainPageContentRepository;
        public PymeManager(IRepository<PymeInfo> pymeInfoRepository, IRepository<PymeContactInfo> pymeContactRepository, IRepository<MainPageContent> mainPageContentRepository)
        {
            _pymeInfoRepository = pymeInfoRepository;
            _pymeContactRepository = pymeContactRepository;
            _mainPageContentRepository = mainPageContentRepository;
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
