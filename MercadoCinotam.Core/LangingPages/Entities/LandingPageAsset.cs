using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.LangingPages.Entities
{
    public class LandingPageAsset : FullAuditedEntity
    {
        public string Source { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
        public virtual LandingPage LandingPage { get; set; }
    }
}
