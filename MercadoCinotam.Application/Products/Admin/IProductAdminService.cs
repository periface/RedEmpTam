using Abp.Application.Services;
using Helpers.GenericTypes;
using MercadoCinotam.Products.Admin.Dtos;
using System;

namespace MercadoCinotam.Products.Admin
{
    public interface IProductAdminService : IApplicationService
    {
        Guid AddProductToStore(ProductInput input);
        ReturnModel<ProductDto> GetProducts(RequestModel request);
        ProductInput GetProductForEdit(Guid? id);
        int AddFeatureToProduct(FeatureInput input);
        AddFeatureInputModel GetAddFeatureViewModel(Guid id);
        void RemoveFeature(int featureId);
    }
}
