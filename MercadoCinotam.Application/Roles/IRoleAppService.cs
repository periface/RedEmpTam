using System.Threading.Tasks;
using Abp.Application.Services;
using MercadoCinotam.Roles.Dto;

namespace MercadoCinotam.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
