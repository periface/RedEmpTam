using Abp.Domain.Services;
using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.Products.Manager
{
    public interface IProductManager : IDomainService
    {
        Guid AddProduct(Product product);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts(int? take = 0);
        int EditProduct(Product product);

        IQueryable<Product> GetQuery();
    }
}
