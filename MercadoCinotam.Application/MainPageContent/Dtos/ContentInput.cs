using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MercadoCinotam.MainPageContent.Dtos
{
    [AutoMap(typeof(MainPageContentManager.Entities.MainPageContent))]
    public class ContentInput : EntityDto, IInputDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsStatic { get; set; }
        public int ThemeReferenceId { get; set; }
        public string ThemeReferenceName { get; set; }
    }
}
