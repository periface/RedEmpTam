using Abp.Application.Services;
using MercadoCinotam.MainPageContent.Dtos;
using System.Threading.Tasks;

namespace MercadoCinotam.MainPageContent.Admin
{
    public interface IMainContentAdminService : IApplicationService
    {
        int AddEditContent(ContentInput input);
        ContentInput GetContentForEdit(int? id);
        Task<int> AddEditContentWithFile(ContentInput model);
    }
}
