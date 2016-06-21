using Abp.Application.Services;

namespace MercadoCinotam.ThemeService
{
    public interface IThemeService : IApplicationService
    {
        string GetActiveThemeFromTenant(string tenant);
    }
}
