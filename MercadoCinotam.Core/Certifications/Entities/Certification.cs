using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Certifications.Entities
{
    public class Certification : FullAuditedEntity, IMustHaveTenant
    {
        public string GalardonName { get; protected set; }
        public string Description { get; protected set; }
        public string UniqueCode { get; protected set; }
        public string Image { get; protected set; }
        public int TenantId { get; set; }

        public static Certification Create(string name, string no, string description)
        {
            return new Certification()
            {
                GalardonName = name,
                UniqueCode = no,
                Description = description
            };
        }

        public void SetImage(string folder)
        {
            Image = folder;
        }
    }
}
