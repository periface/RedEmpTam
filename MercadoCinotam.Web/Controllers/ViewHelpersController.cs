using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class ViewHelpersController : Controller
    {
        // GET: ViewHelpers
        public ActionResult LoadFeaturedProduct()
        {
            return View();
        }

        public ActionResult LoadProductFeatures()
        {
            return View();
        }

        public ActionResult LoadProductGalardons()
        {
            return View();
        }

        public ActionResult AddToCartButton()
        {
            return View();
        }
    }
}