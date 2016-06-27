using Abp.Application.Services.Dto;
using MercadoCinotam.Products.Admin.Dtos;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Client.Dtos
{
    public class ProductList : IOutputDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public string LiClass { get; set; }
        public string UlClass { get; set; }
    }
}
