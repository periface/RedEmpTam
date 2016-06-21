using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace MercadoCinotam.LangingPages.Entities
{
    public class LandingPage : FullAuditedEntity
    {
        public string Html { get; set; }
        public string Active { get; set; }
        public virtual ICollection<LandingPageAsset> LandingPagesAssets { get; set; }
    }
}
