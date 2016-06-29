using Abp.Domain.Entities;

namespace MercadoCinotam.Pyme.Entities
{
    public class PymeContactInfo : Entity, IMustHaveTenant
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
