using Abp.Application.Services;
using MercadoCinotam.Enums;

namespace MercadoCinotam.PymeInfo.PymeClientService
{
    public interface IPymeClientService : IApplicationService
    {
        object GetProperty(string property, PymePropertyListing info);
    }
}
