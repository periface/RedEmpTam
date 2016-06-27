using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Entities
{
    public class Product : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        protected Product()
        {
            Active = false;
        }
        public int TenantId { get; set; }
        public string ProductName { get; protected set; }
        public string ProductDescription { get; protected set; }
        public decimal ProductPrice { get; protected set; }
        public int AvailableStock { get; protected set; }
        public bool Active { get; set; }
        public bool TrackStock { get; set; }
        public string Sku { get; protected set; }
        public string DataImage { get; protected set; }
        public string MainPicture { get; protected set; }
        public string SmallImage { get; protected set; }
        public string MediumImage { get; protected set; }
        public bool IsFeatured { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<ProductCertification> ProductGalardons { get; protected set; }
        public virtual ICollection<ProductFeatureSection> FeatureSections { get; protected set; }
        public virtual ICollection<ProductSlider> ProductSliders { get; protected set; }
        public static Product CreateProduct(string productName, int availableStock, bool enableTrackStock, decimal price, string productDescription, string sku, bool isFeatured, string createSlug)
        {
            return new Product()
            {
                ProductName = productName,
                AvailableStock = availableStock,
                TrackStock = enableTrackStock,
                ProductPrice = price,
                ProductDescription = productDescription,
                Sku = sku,
                IsFeatured = isFeatured
            };
        }

        public void SetSmallImage(string file)
        {
            SmallImage = file;
        }
        public void SetMedImage(string file)
        {
            MediumImage = file;
        }
        public void SetMainImage(string file)
        {
            MainPicture = file;
        }

        public void SetDataImage(string dataImage)
        {
            DataImage = dataImage;
        }

        public void SetSlug(string createSlug)
        {
            Slug = createSlug;
        }
    }
}
