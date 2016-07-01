using Abp.Application.Services;
using MercadoCinotam.MainPageContent.Dtos;

namespace MercadoCinotam.MainPageContent.Admin
{
    public interface IMainContentAdminService : IApplicationService
    {
        int AddEditContent(ContentInput input);
        ContentInput GetContentForEdit(int? id);
    }
}
