using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Products.Entities
{
    public class Feature : FullAuditedEntity
    {
        public string FeatureText { get; set; }
        public virtual ProductFeatureSection FeatureSection { get; set; }
    }
}
