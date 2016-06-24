using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Helpers.GenericTypes;
using MercadoCinotam.Products.Admin;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Web.Controllers;
using System;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class ProductsController : MercadoCinotamControllerBase
    {
        private readonly IProductAdminService _productAdminService;

        public ProductsController(IProductAdminService productAdminService)
        {
            _productAdminService = productAdminService;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]
        public JsonResult GetProducts(RequestModel model)
        {
            ProccessQueryData(model, "ProductName", new[] { "Id", "Sku", "ProductName", "ProductPrice", "Active", "" });
            var products = _productAdminService.GetProducts(model);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddProduct(Guid? id)
        {
            var product = _productAdminService.GetProductForEdit(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductInput model)
        {

            ValidateModel(model);
            if (Request.Files.Count <= 0)
            {
                throw new UserFriendlyException("Image no encontrada");
            }
            model.Imagen = Request.Files[0];
            _productAdminService.AddProductToStore(model);
            return RedirectToAction("Index");
        }

        public ActionResult AddGalardon(Guid? id)
        {
            var galardons = _productAdminService.GetGalardons(id);
            ViewBag.Id = id;
            return View(galardons);
        }
        [HttpPost]
        public ActionResult AddGalardon(GalardonInput input)
        {
            var id = _productAdminService.AddGalardon(input);
            return Json(id);
        }

    }
}