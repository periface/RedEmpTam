using Abp.Application.Services;
using Helpers.GenericTypes;
using MercadoCinotam.PymeInfo.Dtos;
using System.Threading.Tasks;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public interface IPymeAdminService : IApplicationService
    {
        int AddInfo(PymeInfoInput input);
        PymeInfoInput GetInfoForEdit();
        int AddContactInfo(PymeContactInfoInput input);
        PymeContactInfoInput GetContactInfoForEdit();
        Task SetTheme(SetThemeInput input);
        Task<ReturnModel<MainPageContentDto>> GetMainPageContents(RequestModel request, bool onlyActiveTheme = false);
    }
}
