using Abp.Web.Models;
using MercadoCinotam.GalardonsAndCert.Client;
using MercadoCinotam.Products.Client;
using MercadoCinotam.PymeInfo.PymeClientService;
using System;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class ViewHelpersController : Controller
    {
        private readonly IProductClientService _productClientService;
        private readonly IGalardonClientService _galardonClientService;
        private readonly IPymeClientService _pymeClientService;
        public ViewHelpersController(IProductClientService productClientService, IGalardonClientService galardonClientService, IPymeClientService pymeClientService)
        {
            _productClientService = productClientService;
            _galardonClientService = galardonClientService;
            _pymeClientService = pymeClientService;
        }

        // GET: ViewHelpers
        public ActionResult LoadFeaturedProducts(string viewFile)
        {
            var products = _productClientService.GetFeaturedProductList();
            return string.IsNullOrEmpty(viewFile) ? View(products) : View(viewFile, products);
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

        public ActionResult ProductGalardons(Guid id)
        {
            var galardons = _productClientService.GetGalardons(id);
            return View(galardons);
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult AllGalardons()
        {
            var galardons = _galardonClientService.GetAllGalardons();
            return View(galardons);
        }

        public ActionResult GetProductPropertyValue(string productSlug, string property)
        {
            var value = _productClientService.GetProperty(productSlug, property);
            return View(new MvcHtmlString(value.ToString()));
        }

        public ActionResult GetPymePropertyValue(string property)
        {
            var value = _pymeClientService.GetProperty(property);
            return View(new MvcHtmlString(value.ToString()));
        }
        public ActionResult GetFeatures(Guid id)
        {
            var features = _productClientService.GetProductFeatures(id);
            return View(features);
        }


        [WrapResult(false)]
        public JsonResult GetPropertiesFromMain(string key)
        {
            var propertyValue = _pymeClientService.GetPropertyFromMainContent(key);
            return Json(propertyValue, JsonRequestBehavior.AllowGet);
        }
        [WrapResult(false)]
        public JsonResult GetPropertiesFromPymeInfo(string key)
        {
            var propertyValue = _pymeClientService.GetProperty(key);
            return Json(propertyValue, JsonRequestBehavior.AllowGet);
        }
    }
}