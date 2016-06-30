using Abp.Application.Services.Dto;

namespace MercadoCinotam.PymeInfo.Dtos
{
    public class MainPageContentDto : EntityDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
