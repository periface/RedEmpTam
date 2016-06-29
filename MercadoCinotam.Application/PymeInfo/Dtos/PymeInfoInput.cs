using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Web;

namespace MercadoCinotam.PymeInfo.Dtos
{
    [AutoMap(typeof(Pyme.Entities.PymeInfo))]
    public class PymeInfoInput : EntityDto, IInputDto
    {
        public string PymeName { get; set; }
        public string PymeLogo { get; set; }
        public string PymeSlogan { get; set; }
        public string About { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
