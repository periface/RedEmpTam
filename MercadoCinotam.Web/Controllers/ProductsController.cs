using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class ProductsController : MercadoCinotamControllerBase
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [Route("Details/{slug}/{id}")]
        public ActionResult Details(string slug, string id)
        {
            return View();
        }
    }
}