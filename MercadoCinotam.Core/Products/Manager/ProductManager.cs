using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Helpers.TenancyHelpers;
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
        private readonly IRepository<ProductCertification> _galardonsRepository;
        public ProductManager(IRepository<Product, Guid> productRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<ProductCertification> galardonsRepository)
        {
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _galardonsRepository = galardonsRepository;
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
                return _productRepository.GetAll().Where(a => a.TenantId == TenantHelper.TenantId).Take(take.Value).ToList();
            }
            return _productRepository.GetAllList(a => a.TenantId == TenantHelper.TenantId);
        }
        public int EditProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetQuery()
        {
            return _productRepository.GetAll();
        }

        public int AddGalardon(ProductCertification galardon)
        {
            var id = _galardonsRepository.InsertOrUpdateAndGetId(galardon);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public ProductCertification GetGalardon(int galardonId)
        {
            var galardon = _galardonsRepository.FirstOrDefault(a => a.Id == galardonId);
            return galardon;
        }

        public IEnumerable<Product> GetFeaturedProducts()
        {
            var products = _productRepository.GetAll().Where(a => a.Active && a.IsFeatured && a.TenantId == TenantHelper.TenantId);
            return products.ToList();
        }

    }
}
