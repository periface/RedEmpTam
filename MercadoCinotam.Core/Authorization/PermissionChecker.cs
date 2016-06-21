using Abp.Authorization;
using MercadoCinotam.Authorization.Roles;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.Users;

namespace MercadoCinotam.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
