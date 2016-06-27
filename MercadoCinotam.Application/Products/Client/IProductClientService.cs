using Abp.Application.Services;
using MercadoCinotam.Products.Client.Dtos;

namespace MercadoCinotam.Products.Client
{
    public interface IProductClientService : IApplicationService
    {
        ProductList GetProductList();
        ProductList GetFeaturedProductList();
        ProductList GetProductListWithTake(int take);
    }
}
