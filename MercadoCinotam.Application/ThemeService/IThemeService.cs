using Abp.Application.Services;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService
{
    public interface IThemeService : IApplicationService
    {
        Task<string> GetActiveThemeFromTenant();
    }
}
