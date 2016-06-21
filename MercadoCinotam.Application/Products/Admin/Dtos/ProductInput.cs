using Abp.Application.Services.Dto;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class ProductInput : IInputDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }
}
