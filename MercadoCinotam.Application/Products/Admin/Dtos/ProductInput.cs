using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Products.Entities;
using System;
using System.Web;

namespace MercadoCinotam.Products.Admin.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductInput : EntityDto<Guid?>, IInputDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int AvailableStock { get; set; }
        public bool TrackStock { get; set; }
        public string DataImage { get; set; }
        public string MainPicture { get; set; }
        public string SmallImage { get; set; }
        public string MediumImage { get; set; }
        public string Sku { get; set; }
        public HttpPostedFileBase Imagen { get; set; }
        public bool IsFeatured { get; set; }
    }
}
