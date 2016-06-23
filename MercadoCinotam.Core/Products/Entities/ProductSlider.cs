using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Products.Entities
{
    public class ProductSlider : FullAuditedEntity
    {
        public string Text { get; set; }
        public string Image { get; set; }
        public bool EnableText { get; set; }
        public virtual Product Product { get; set; }

    }
}
