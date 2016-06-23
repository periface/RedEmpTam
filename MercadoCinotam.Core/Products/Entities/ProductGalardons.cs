using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Products.Entities
{
    public class ProductGalardons : FullAuditedEntity, IMustHaveTenant
    {
        public string GalardonName { get; protected set; }
        public string Description { get; protected set; }
        public string UniqueCode { get; protected set; }
        public string Image { get; protected set; }
        public int TenantId { get; set; }
        public virtual Product Product { get; protected set; }

        public static ProductGalardons Create(string name, string no, Product product)
        {
            return new ProductGalardons()
            {
                GalardonName = name,
                UniqueCode = no,
                Product = product
            };
        }
    }
}
