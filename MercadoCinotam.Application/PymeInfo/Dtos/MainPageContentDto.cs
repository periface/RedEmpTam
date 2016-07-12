using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MercadoCinotam.PymeInfo.Dtos
{
    [AutoMap(typeof(MainPageContentManager.Entities.MainPageContent))]
    public class MainPageContentDto : EntityDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsStatic { get; set; }
        public string ThemeReferenceId { get; set; }
        public string ThemeReferenceName { get; set; }
    }
}
