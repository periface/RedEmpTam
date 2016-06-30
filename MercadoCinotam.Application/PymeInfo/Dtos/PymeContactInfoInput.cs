using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Pyme.Entities;

namespace MercadoCinotam.PymeInfo.Dtos
{
    [AutoMap(typeof(PymeContactInfo))]
    public class PymeContactInfoInput : EntityDto, IInputDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public int TenantId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
