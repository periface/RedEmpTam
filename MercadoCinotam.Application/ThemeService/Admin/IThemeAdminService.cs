using MercadoCinotam.ThemeService.Dtos;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Admin
{
    public interface IThemeAdminService
    {
        Task<ThemeSelectorOutput> GetThemesForSelector();

    }
}
