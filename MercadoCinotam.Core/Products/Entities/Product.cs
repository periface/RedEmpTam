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
        public string DataImage { get; protected set; }
        public string MainPicture { get; protected set; }
        public string SmallImage { get; protected set; }
        public string MediumImage { get; protected set; }
        public virtual ICollection<ProductGalardons> ProductGalardons { get; protected set; }

        public static Product CreateProduct(string productName, int availableStock, bool enableTrackStock, decimal price, string productDescription)
        {
            return new Product()
            {
                ProductName = productName,
                AvailableStock = availableStock,
                TrackStock = enableTrackStock,
                ProductPrice = price,
                ProductDescription = productDescription
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
    }
}
