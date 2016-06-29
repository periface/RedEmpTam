using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Helpers.TenancyHelpers;
using MercadoCinotam.Pyme.Entities;
using System;

namespace MercadoCinotam.Pyme.Manager
{
    public class PymeManager : DomainService, IPymeManager
    {
        private readonly IRepository<PymeInfo> _pymeInfoRepository;

        public PymeManager(IRepository<PymeInfo> pymeInfoRepository)
        {
            _pymeInfoRepository = pymeInfoRepository;
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
            throw new NotImplementedException();
        }

        public PymeInfo GetInfo()
        {
            var info = _pymeInfoRepository.FirstOrDefault(a => a.TenantId == TenantHelper.TenantId);
            return info;
        }
    }
}
