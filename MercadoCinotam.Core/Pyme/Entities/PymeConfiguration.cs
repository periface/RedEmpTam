using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace MercadoCinotam.Pyme.Entities
{
    public class PymeConfiguration : Entity, IModificationAudited
    {
        public bool UseManyProductsConfiguration { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
