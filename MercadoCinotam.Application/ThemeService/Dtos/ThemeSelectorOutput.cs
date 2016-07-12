using Abp.Application.Services.Dto;
using Helpers.Helpers.HelperModels;
using System.Collections.Generic;

namespace MercadoCinotam.ThemeService.Dtos
{
    public class ThemeSelectorOutput : IOutputDto
    {
        public ThemeInfo ActiveTheme { get; set; }
        public List<ThemeDto> Themes { get; set; }
    }
    public class ThemeDto : EntityDto<string>
    {
        public string ThemeUniqueName { get; set; }
        public string ThemeName { get; set; }
        public string ThemeDescription { get; set; }
        public bool Released { get; set; }
        public IEnumerable<ThemePreviewDto> Preview { get; set; }

    }
    public class ThemePreviewDto
    {
        public string Image { get; set; }
    }
}
