using Abp.Application.Services.Dto;
using System;
using System.Web;

namespace MercadoCinotam.Products.Admin.Dtos
{
    public class GalardonInput : IInputDto
    {
        public Guid? ProductId { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
