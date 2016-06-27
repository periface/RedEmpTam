using Abp.Configuration;
using System.Collections.Generic;

namespace MercadoCinotam
{
    public class SystemThemeProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    "Theme",
                    "SimpleTheme",
                    scopes:SettingScopes.Tenant
                    ),
            };
        }
    }
}