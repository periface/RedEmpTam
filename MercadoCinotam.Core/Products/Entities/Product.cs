using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Entities
{
    public class Product : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal AvailableStock { get; set; }
        public virtual ICollection<ProductGalardons> ProductGalardons { get; set; }

    }
}
