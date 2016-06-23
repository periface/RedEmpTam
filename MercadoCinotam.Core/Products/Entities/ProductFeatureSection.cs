﻿using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Entities
{
    public class ProductFeatureSection : FullAuditedEntity
    {
        public string Position { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDescription { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Feature> Features { get; set; }

    }
}
