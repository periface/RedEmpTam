using Abp.Application.Services;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Client
{
    public interface IThemeClientService : IApplicationService
    {
        Task<string> GetActiveThemeFromTenant();
    }
}
