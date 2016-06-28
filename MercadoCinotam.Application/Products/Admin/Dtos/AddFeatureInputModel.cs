using MercadoCinotam.Products.Client.Dtos;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class AddFeatureInputModel
    {
        public string Feature { get; set; }
        public Guid ProductId { get; set; }
        public List<FeatureDto> Features { get; set; }
    }
}
