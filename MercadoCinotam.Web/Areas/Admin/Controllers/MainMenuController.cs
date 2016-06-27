using Abp.Web.Mvc.Authorization;
using MercadoCinotam.Web.Controllers;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class MainMenuController : MercadoCinotamControllerBase
    {
        // GET: Admin/MainMenu
        public ActionResult Index()
        {
            return View();
        }
    }
}