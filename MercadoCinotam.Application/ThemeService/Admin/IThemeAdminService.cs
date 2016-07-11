using Abp.Application.Services;
using MercadoCinotam.ThemeService.Dtos;
using System.Threading.Tasks;

namespace MercadoCinotam.ThemeService.Admin
{
    public interface IThemeAdminService : IApplicationService
    {
        Task<ThemeSelectorOutput> GetThemesForSelector();

        Task<ThemeHtmlInput> GetThemeContentForEdit();
        Task CreateHtml(ThemeHtmlInput input);
    }
}
