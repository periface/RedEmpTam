using Abp.Application.Services.Dto;

namespace MercadoCinotam.ThemeService.Dtos
{
    public class ThemeHtmlInput : IInputDto
    {
        public string HtmlContentBody { get; set; }
        public string HtmlContentHeader { get; set; }
    }
}
