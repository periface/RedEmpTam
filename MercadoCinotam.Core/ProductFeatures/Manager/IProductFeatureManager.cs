using Abp.Domain.Services;
using MercadoCinotam.ProductFeatures.Entities;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.ProductFeatures.Manager
{
    public interface IProductFeatureManager : IDomainService
    {
        IEnumerable<Feature> GetProductFeatures(Guid productId);
        int AddProductFeature(string feature, Guid productId);
        void RemoveFeature(int featureId);
    }
}
