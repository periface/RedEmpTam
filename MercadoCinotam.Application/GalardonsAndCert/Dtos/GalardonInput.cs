using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Certifications.Entities;
using System.Web;

namespace MercadoCinotam.GalardonsAndCert.Dtos
{
    [AutoMap(typeof(Certification))]
    public class GalardonInput : EntityDto, IInputDto
    {
        public string GalardonName { get; set; }
        public string Description { get; set; }
        public string UniqueCode { get; set; }
        public string Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public string ReturnUrl { get; set; }
    }
}