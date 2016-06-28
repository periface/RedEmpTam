using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Helpers.TenancyHelpers;
using MercadoCinotam.Certifications.Entities;
using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.Certifications.Manager
{
    public class CertificationManager : ICertificationManager
    {
        private readonly IRepository<Certification> _certificationRepository;
        private readonly IRepository<ProductCertification> _productCertificationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public CertificationManager(IRepository<Certification> certificationRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<ProductCertification> productCertificationRepository)
        {
            _certificationRepository = certificationRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _productCertificationRepository = productCertificationRepository;
        }

        public int AddCertification(Certification input)
        {
            var id = _certificationRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public Certification GetCertification(int certificationId)
        {
            return _certificationRepository.FirstOrDefault(a => a.Id == certificationId);
        }

        public IEnumerable<Certification> GetCertifications()
        {
            var certifications = _certificationRepository.GetAllList(a => a.TenantId == TenantHelper.TenantId);
            return certifications;
        }

        public IQueryable<Certification> GetCertificationsQuery()
        {
            return _certificationRepository.GetAll();
        }

        public IEnumerable<ProductCertification> GetCertifications(Guid productId)
        {
            var certifications = _productCertificationRepository.GetAllList(a => a.ProductId == productId);
            return certifications;
        }

        public IEnumerable<Certification> GetCertifications(IEnumerable<ProductCertification> exclude)
        {
            var certifications = new List<Certification>();
            foreach (var excluded in exclude)
            {
                var ex = _certificationRepository.Get(excluded.CertId);
                certifications.Add(ex);
            }
            return _certificationRepository.GetAll().ToList().Except(certifications);
        }

        public void AddCertificationToProduct(Guid productId, int id)
        {
            var coincidence = _productCertificationRepository.GetAllList(a => a.CertId == id && a.ProductId == productId);
            if (coincidence.Any()) return;
            var relation = ProductCertification.Create(id, productId);
            _productCertificationRepository.InsertOrUpdateAndGetId(relation);
        }
    }
}
