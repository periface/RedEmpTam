using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Certifications.Entities;

namespace MercadoCinotam.GalardonsAndCert.Dtos
{
    [AutoMap(typeof(Certification))]
    public class GalardonDto : EntityDto, IOutputDto
    {
        public string GalardonName { get; set; }
        public string Description { get; set; }
        public string UniqueCode { get; set; }
        public string Image { get; set; }
    }
}
