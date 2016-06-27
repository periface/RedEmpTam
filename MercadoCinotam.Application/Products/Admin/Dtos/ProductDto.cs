using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Products.Entities;
using System;

namespace MercadoCinotam.Products.Admin.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto<Guid>
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Active { get; set; }
        public string Sku { get; set; }
        public bool IsFeatured { get; set; }
        public string MainPicture { get; set; }
        public string SmallImage { get; set; }
        public string MediumImage { get; set; }
        public string Slug { get; set; }
    }
}
