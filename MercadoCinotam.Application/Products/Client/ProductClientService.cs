using Abp.AutoMapper;
using MercadoCinotam.Certifications;
using MercadoCinotam.GalardonsAndCert.Dtos;
using MercadoCinotam.ProductFeatures;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Client.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.Products.Client
{
    public class ProductClientService : IProductClientService
    {
        private readonly ProductProvider _productManager;
        private readonly CertificationProvider _certificationManager;
        private readonly ProductFeaturesProvider _productFeatureManager;
        public ProductClientService(ProductProvider productManager,
            CertificationProvider certificationManager,
            ProductFeaturesProvider productFeatureManager)
        {
            _productManager = productManager;
            _certificationManager = certificationManager;
            _productFeatureManager = productFeatureManager;
        }

        public ProductList GetProductList()
        {
            throw new NotImplementedException();
        }

        public ProductList GetFeaturedProductList()
        {
            var products = _productManager.GetFeaturedProducts();
            return new ProductList()
            {
                Products = products.Select(a => a.MapTo<ProductDto>())
            };
        }

        public ProductList GetProductListWithTake(int take)
        {
            var products = _productManager.GetProducts(take);
            return new ProductList()
            {
                Products = products.Select(a => a.MapTo<ProductDto>())
            };
        }

        public ProductDto GetProduct(string slug, string id)
        {
            var product = _productManager.GetProduct(id, slug);
            return product.MapTo<ProductDto>();
        }

        public GalardonList GetGalardons(Guid id)
        {
            var galardons = _certificationManager.GetCertifications(id);
            var model = new GalardonList()
            {
                Galardons = new List<GalardonDto>()
            };
            foreach (var productCertification in galardons)
            {
                var galardonObj = _certificationManager.GetCertification(productCertification.CertId);
                model.Galardons.Add(galardonObj.MapTo<GalardonDto>());
            }
            return model;
        }

        public object GetProperty(string productSlug, string property)
        {
            var product = _productManager.GetProduct(productSlug: productSlug);
            var prop = product.GetType().GetProperty(property).GetValue(product);
            return prop;
        }

        public FeaturesList GetProductFeatures(Guid id)
        {
            var features = _productFeatureManager.GetProductFeatures(id);
            return new FeaturesList()
            {
                Features = features.Select(a => a.MapTo<FeatureDto>()).ToList()
            };
        }
    }
}
