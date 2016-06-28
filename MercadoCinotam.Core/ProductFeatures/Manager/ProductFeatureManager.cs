using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MercadoCinotam.ProductFeatures.Entities;
using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.ProductFeatures.Manager
{
    public class ProductFeatureManager : DomainService, IProductFeatureManager
    {
        private readonly IRepository<Feature> _featureRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductFeatureManager(IRepository<Feature> featureRepository, IRepository<Product, Guid> productRepository)
        {
            _featureRepository = featureRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<Feature> GetProductFeatures(Guid productId)
        {
            var features = _featureRepository.GetAllList(a => a.Product.Id == productId);
            return features;
        }
        public int AddProductFeature(string feature, Guid productId)
        {
            var product = _productRepository.Get(productId);
            var id = _featureRepository.InsertOrUpdateAndGetId(new Feature()
            {
                FeatureText = feature,
                Product = product
            });
            CurrentUnitOfWork.SaveChanges();
            return id;
        }

        public void RemoveFeature(int featureId)
        {
            _featureRepository.Delete(featureId);
        }
    }
}
