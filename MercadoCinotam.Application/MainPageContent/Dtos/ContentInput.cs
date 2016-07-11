using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Castle.Components.DictionaryAdapter;
using System.Collections.Generic;
using System.Web;

namespace MercadoCinotam.MainPageContent.Dtos
{
    [AutoMap(typeof(MainPageContentManager.Entities.MainPageContent))]
    public class ContentInput : EntityDto, IInputDto
    {
        public ContentInput()
        {
            Types = new EditableList<MainContentType>()
            {
                new MainContentType()
                {
                    Type = "Texto"
                },
                new MainContentType()
                {
                    Type = "Imagen"
                }
            };
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsStatic { get; set; }
        public int ThemeReferenceId { get; set; }
        public string ThemeReferenceName { get; set; }
        public List<MainContentType> Types { get; set; }
        public HttpPostedFileBase File   { get; set; }
    }
}
