using Abp.Domain.Entities;

namespace MercadoCinotam.Pyme.Entities
{
    public class PymeOwner : Entity, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string SurName { get; set; }
        public string Bios { get; set; }
        public int TenantId { get; set; }
        public bool DisplayOnMainPage { get; set; }
    }
}
