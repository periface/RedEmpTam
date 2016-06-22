using Abp.Application.Services.Dto;
using System;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class ProductDto : EntityDto<Guid>
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Active { get; set; }
    }
}
