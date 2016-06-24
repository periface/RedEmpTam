using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.GalardonsAndCert.Dtos
{

    public class GalardonProductInput : EntityDto, IInputDto
    {
        public IEnumerable<GalardonDto> ActiveGalardonDtos { get; set; }
        public IEnumerable<GalardonDto> InActiveGalardonDtos { get; set; }
        public int[] Actives { get; set; }
        public Guid ProductId { get; set; }
    }
}
