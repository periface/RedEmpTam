using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Client;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class ProductsController : MercadoCinotamControllerBase
    {
        private IProductClientService _productClientService;

        public ProductsController(IProductClientService productClientService)
        {
            _productClientService = productClientService;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [Route("Details/{slug}/{id}")]
        public ActionResult Details(string slug, string id)
        {
            ProductDto product = _productClientService.GetProduct(slug, id);
            if (product == null) return HttpNotFound();
            return View(product);
        }
    }
}