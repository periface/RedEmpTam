using MercadoCinotam.Products.Client;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class ViewHelpersController : Controller
    {
        private readonly IProductClientService _productClientService;

        public ViewHelpersController(IProductClientService productClientService)
        {
            _productClientService = productClientService;
        }

        // GET: ViewHelpers
        public ActionResult LoadFeaturedProducts(string ulClass = "", string liClass = "")
        {
            var products = _productClientService.GetFeaturedProductList();
            products.UlClass = ulClass;
            products.LiClass = liClass;
            return View(products);
        }

        public ActionResult LoadMainProduct()
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

        public ActionResult LoadMoreProducts(int take)
        {
            var products = _productClientService.GetProductListWithTake(take);
            return View(products);
        }
    }
}