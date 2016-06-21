using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Manager
{
    public interface IProductManager
    {
        int AddProduct(Product product);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts(int? take = 0);
    }
}
