using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Helpers.GenericTypes;
using MercadoCinotam.GalardonsAndCert.Admin;
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
        private readonly IGalardonAdminService _galardonAdminService;
        public ProductsController(IProductAdminService productAdminService, IGalardonAdminService galardonAdminService)
        {
            _productAdminService = productAdminService;
            _galardonAdminService = galardonAdminService;
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

        public ActionResult AddFeature(Guid id)
        {
            var model = _productAdminService.GetAddFeatureViewModel(id);
            return View(model);
        }
        public ActionResult AddGalardon(Guid? id)
        {
            if (id == null) return HttpNotFound();
            var galardons = _galardonAdminService.GetGalardonAssignationModel(id.Value);
            ViewBag.Id = id;
            return View(galardons);
        }
    }
}