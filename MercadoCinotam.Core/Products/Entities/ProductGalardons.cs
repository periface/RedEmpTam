using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Products.Entities
{
    public class ProductGalardons : FullAuditedEntity, IMustHaveTenant
    {
        public string GalardonName { get; set; }
        public string Description { get; set; }
        public string UniqueCode { get; set; }
        public string Image { get; set; }
        public int TenantId { get; set; }
        public virtual Product Product { get; set; }
    }
}
