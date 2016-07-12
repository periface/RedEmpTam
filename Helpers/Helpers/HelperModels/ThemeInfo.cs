using System;
using System.Collections.Generic;

namespace Helpers.Helpers.HelperModels
{
    public class ThemeInfo
    {
        public string ThemeName { get; set; }
        public string ThemeUniqueName { get; set; }
        public int? TenantId { get; set; }
        //Tenant names are unique too
        public string TenantName { get; set; }
        public string ThemeDescription { get; set; }
        public bool InDevelopment { get; set; }
        public string PreviewsFolder { get; set; }

        public DateTime ThemeCreationTime { get; set; }
        public List<PropertyService> PropertyServices { get; set; }
        public List<IterationService> IterationServices { get; set; }
        public List<ThemeRequiredProperty> ThemeRequiredProperties { get; set; }
    }

    public class PropertyService
    {
        public string Name { get; set; }
    }

    public class IterationService
    {
        public string Name { get; set; }
        public string[] Properties { get; set; }
    }

    public class ThemeRequiredProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
