using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MercadoCinotam.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.Products.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductManager(IRepository<Product, Guid> productRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public Guid AddProduct(Product product)
        {
            var id = _productRepository.InsertOrUpdateAndGetId(product);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public Product GetProduct(Guid id)
        {
            var product = _productRepository.Get(id);
            return product;
        }

        public IEnumerable<Product> GetProducts(int? take)
        {
            if (take.HasValue)
            {
                return _productRepository.GetAll().Take(take.Value);
            }
            return _productRepository.GetAllList();
        }
        public int EditProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetQuery()
        {
            return _productRepository.GetAll();
        }
    }
}
