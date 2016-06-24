using Abp.Web.Models;
using Helpers.GenericTypes;
using MercadoCinotam.GalardonsAndCert;
using MercadoCinotam.GalardonsAndCert.Dtos;
using MercadoCinotam.Web.Controllers;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    public class CertAndGalController : MercadoCinotamControllerBase
    {
        private readonly IGalardonAdminService _galardonAdminService;

        public CertAndGalController(IGalardonAdminService galardonAdminService)
        {
            _galardonAdminService = galardonAdminService;
        }

        // GET: Admin/CertAndGal
        public ActionResult Index()
        {

            return View();
        }

        [WrapResult(false)]
        public JsonResult GetCertifications(RequestModel input)
        {
            ProccessQueryData(input, "GalardonName", new[] { "Id", "GalardonName", "UniqueCode", "" });
            var data = _galardonAdminService.GetGalardons(input);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(int? id, string returnUrl)
        {
            var model = _galardonAdminService.GetGalardonForEdit(id);
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GalardonInput input)
        {

            ValidateModel(input);
            input.ImageFile = Request.Files[0];
            _galardonAdminService.AddGalardon(input);
            if (!string.IsNullOrEmpty(input.ReturnUrl))
            {
                return Redirect(input.ReturnUrl);
            }
            return RedirectToAction("Index");
        }
    }
}