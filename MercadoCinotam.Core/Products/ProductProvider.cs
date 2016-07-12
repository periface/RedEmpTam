using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MercadoCinotam.Products.Entities;
using MercadoCinotam.Products.Manager;
using System;

namespace MercadoCinotam.Products
{
    public class ProductProvider : ProductManager
    {
        public ProductProvider(
            IRepository<Product, Guid> productRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<ProductCertification> galardonsRepository)
            : base(productRepository,
                  unitOfWorkManager,
                  galardonsRepository)
        {
        }
    }
}
