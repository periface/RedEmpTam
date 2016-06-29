using Helpers.ReflectionHelpers;
using MercadoCinotam.PymeInfo.Dtos;
using MercadoCinotam.PymeInfo.PymeAdminService;
using MercadoCinotam.Web.Controllers;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    public class MyStoreController : MercadoCinotamControllerBase
    {
        private readonly IPymeAdminService _pymeAdminService;

        public MyStoreController(IPymeAdminService pymeAdminService)
        {
            _pymeAdminService = pymeAdminService;
        }

        // GET: Admin/MyStore
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainInfo()
        {
            var info = _pymeAdminService.GetInfoForEdit();
            return View(info);
        }

        [HttpPost]
        public ActionResult MainInfo(FormCollection forms)
        {
            var model = InputBuilder.BuildInputByRequest<PymeInfoInput>(Request);
            if (Request.Files.Count > 0)
            {
                model.Image = Request.Files[0];
            }
            return Json(_pymeAdminService.AddInfo(model), JsonRequestBehavior.AllowGet);
        }
    }
}