using Abp.Application.Services;
using MercadoCinotam.Products.Client.Dtos;

namespace MercadoCinotam.GalardonsAndCert.Client
{
    public interface IGalardonClientService : IApplicationService
    {
        GalardonList GetAllGalardons();
    }
}
