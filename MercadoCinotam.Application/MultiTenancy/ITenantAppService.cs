using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MercadoCinotam.MultiTenancy.Dto;
using System.Threading.Tasks;

namespace MercadoCinotam.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultOutput<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
        Task<int?> GetTenantIdByName(string tenancyName);
        Task<string> GetTenantNameById(int tenantId);
    }
}
