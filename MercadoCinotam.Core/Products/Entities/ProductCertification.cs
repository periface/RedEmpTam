using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace MercadoCinotam.Products.Entities
{
    public class ProductCertification : FullAuditedEntity, IMustHaveTenant
    {
        public Guid ProductId { get; set; }
        public int CertId { get; set; }
        public int TenantId { get; set; }

        public static ProductCertification Create(int certId, Guid productId)
        {
            return new ProductCertification()
            {
                ProductId = productId,
                CertId = certId
            };
        }
    }
}
