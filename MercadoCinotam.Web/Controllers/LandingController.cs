using Helpers.ThemeHelper;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class LandingController : MercadoCinotamControllerBase
    {
        // GET: Landing
        public ActionResult Index()
        {
            ThemeHelper.ClearTheme(System.Web.HttpContext.Current);
            return View();
        }
    }
}