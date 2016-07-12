using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MercadoCinotam.Certifications.Entities;
using MercadoCinotam.Certifications.Manager;
using MercadoCinotam.Products.Entities;

namespace MercadoCinotam.Certifications
{
    public class CertificationProvider : CertificationManager
    {
        public CertificationProvider(IRepository<Certification> certificationRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<ProductCertification> productCertificationRepository) : base(certificationRepository, unitOfWorkManager, productCertificationRepository)
        {
        }
    }
}
