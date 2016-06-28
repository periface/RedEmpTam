using Abp.Domain.Entities.Auditing;
using MercadoCinotam.Products.Entities;

namespace MercadoCinotam.ProductFeatures.Entities
{
    public class Feature : FullAuditedEntity
    {
        public string FeatureText { get; set; }
        public Product Product { get; set; }

        //public virtual ProductFeatureSection FeatureSection { get; set; }
    }
}
