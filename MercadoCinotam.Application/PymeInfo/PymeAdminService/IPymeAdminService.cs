using Abp.Application.Services;
using MercadoCinotam.PymeInfo.Dtos;
using System.Threading.Tasks;
using Helpers.GenericTypes;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public interface IPymeAdminService : IApplicationService
    {
        int AddInfo(PymeInfoInput input);
        PymeInfoInput GetInfoForEdit();
        int AddContactInfo(PymeContactInfoInput input);
        PymeContactInfoInput GetContactInfoForEdit();
        Task SetTheme(SetThemeInput input);
        ReturnModel<MainPageContentDto> GetMainPageContents(RequestModel request);
    }
}
