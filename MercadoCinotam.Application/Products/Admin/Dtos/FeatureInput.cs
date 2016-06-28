using Abp.Application.Services.Dto;
using System;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class FeatureInput : EntityDto, IInputDto
    {
        public string Feature { get; set; }
        public Guid ProductId { get; set; }
    }
}
