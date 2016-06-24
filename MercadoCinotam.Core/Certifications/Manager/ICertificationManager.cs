using Abp.Domain.Services;
using MercadoCinotam.Certifications.Entities;
using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.Certifications.Manager
{
    public interface ICertificationManager : IDomainService
    {
        int AddCertification(Certification input);
        Certification GetCertification(int certificationId);
        IEnumerable<Certification> GetCertifications();

        IQueryable<Certification> GetCertificationsQuery();
        IEnumerable<ProductCertification> GetCertifications(Guid productId);
        IEnumerable<Certification> GetCertifications(IEnumerable<ProductCertification> exclude);
        void AddCertificationToProduct(Guid productId, int id);
    }
}
