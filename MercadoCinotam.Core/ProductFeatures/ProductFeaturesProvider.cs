using Abp.Domain.Repositories;
using MercadoCinotam.ProductFeatures.Entities;
using MercadoCinotam.ProductFeatures.Manager;
using MercadoCinotam.Products.Entities;
using System;

namespace MercadoCinotam.ProductFeatures
{
    public class ProductFeaturesProvider : ProductFeatureManager
    {
        public ProductFeaturesProvider(
            IRepository<Feature> featureRepository,
            IRepository<Product, Guid> productRepository)
            : base(featureRepository, productRepository)
        {
        }
    }
}
