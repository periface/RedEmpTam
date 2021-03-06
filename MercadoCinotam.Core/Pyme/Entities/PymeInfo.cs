﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MercadoCinotam.Pyme.Entities
{
    public class PymeInfo : FullAuditedEntity, IMustHaveTenant
    {
        public string PymeName { get; set; }
        public string PymeLogo { get; set; }
        public string PymeSlogan { get; set; }
        public string About { get; set; }
        public int TenantId { get; set; }
    }
}
