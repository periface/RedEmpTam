using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MercadoCinotam.Themes.Entities;
using System.Collections.Generic;

namespace MercadoCinotam.ThemeService.Dtos
{
    public class ThemeSelectorOutput : IOutputDto
    {
        public ThemeDto ActiveTheme { get; set; }
        public List<ThemeDto> Themes { get; set; }
    }
    [AutoMapFrom(typeof(Theme))]
    public class ThemeDto : EntityDto
    {
        public string ThemeUniqueName { get; set; }
        public string ThemeName { get; set; }
        public string ThemeDescription { get; set; }
        public bool Released { get; set; }
        public IEnumerable<ThemePreviewDto> Preview { get; set; }
    }
    [AutoMap(typeof(ThemePreview))]
    public class ThemePreviewDto
    {
        public string Image { get; set; }
    }
}
