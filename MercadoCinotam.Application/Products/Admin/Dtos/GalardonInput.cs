using Abp.Application.Services.Dto;
using System;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class GalardonInput : EntityDto, IInputDto
    {
        public Guid ProductId { get; set; }
        public int GalardonId { get; set; }
    }
}
