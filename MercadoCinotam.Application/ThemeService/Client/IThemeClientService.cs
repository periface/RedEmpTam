using Abp.Application.Services;
using MercadoCinotam.ThemeService.Dtos;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Client
{
    public interface IThemeClientService : IApplicationService
    {
        Task<string> GetActiveThemeFromTenant();
        Task<ThemeContentOutput> GetThemeContentFor(string selector);
    }
}
