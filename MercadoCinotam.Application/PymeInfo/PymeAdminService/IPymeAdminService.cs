using Abp.Application.Services;
using MercadoCinotam.PymeInfo.Dtos;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public interface IPymeAdminService : IApplicationService
    {
        int AddInfo(PymeInfoInput input);
        PymeInfoInput GetInfoForEdit();
    }
}
