using Abp.Application.Services;
using Helpers.GenericTypes;
using MercadoCinotam.Products.Admin.Dtos;
using System;
using System.Web;

namespace MercadoCinotam.Products.Admin
{
    public interface IProductAdminService : IApplicationService
    {
        Guid AddProductToStore(ProductInput input, HttpPostedFileBase httpPostedFileBase);
        ReturnModel<ProductDto> GetProducts(RequestModel request);
        ProductInput GetProductForEdit(Guid? id);
        object GetGalardons(Guid? id);
    }
}
