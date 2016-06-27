using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class LandingController : MercadoCinotamControllerBase
    {
        // GET: Landing
        public ActionResult Index()
        {
            return View();
        }
    }
}