using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.ProductFeatures.Entities;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Client.Dtos
{
    public class FeaturesList : IOutputDto
    {
        public List<FeatureDto> Features { get; set; }
    }
    [AutoMap(typeof(Feature))]
    public class FeatureDto : EntityDto
    {
        public string FeatureText { get; set; }
    }
}
