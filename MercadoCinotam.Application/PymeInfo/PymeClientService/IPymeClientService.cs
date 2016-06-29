using Abp.Application.Services;

namespace MercadoCinotam.PymeInfo.PymeClientService
{
    public interface IPymeClientService : IApplicationService
    {
        object GetProperty(string property);
    }
}
