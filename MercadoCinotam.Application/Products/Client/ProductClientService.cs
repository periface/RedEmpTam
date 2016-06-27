using Abp.AutoMapper;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Client.Dtos;
using MercadoCinotam.Products.Manager;
using System;
using System.Linq;

namespace MercadoCinotam.Products.Client
{
    public class ProductClientService : IProductClientService
    {
        private readonly IProductManager _productManager;

        public ProductClientService(IProductManager productManager)
        {
            _productManager = productManager;
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
    }
}
