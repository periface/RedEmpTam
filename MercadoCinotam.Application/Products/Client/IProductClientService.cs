using Abp.Application.Services;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Client.Dtos;
using System;

namespace MercadoCinotam.Products.Client
{
    public interface IProductClientService : IApplicationService
    {
        ProductList GetProductList();
        ProductList GetFeaturedProductList();
        ProductList GetProductListWithTake(int take);
        ProductDto GetProduct(string slug, string id);
        GalardonList GetGalardons(Guid id);
        object GetProperty(string productSlug, string property);
        FeaturesList GetProductFeatures(Guid id);
    }
}
