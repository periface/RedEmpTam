using Abp.Application.Services.Dto;
using MercadoCinotam.GalardonsAndCert.Dtos;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Client.Dtos
{
    public class GalardonList : IOutputDto
    {
        public List<GalardonDto> Galardons { get; set; }
    }
}
