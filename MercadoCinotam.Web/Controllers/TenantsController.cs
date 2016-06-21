using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using MercadoCinotam.Authorization;
using MercadoCinotam.MultiTenancy;

namespace MercadoCinotam.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : MercadoCinotamControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}