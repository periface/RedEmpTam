using Abp.Web.Models;
using Helpers.GenericTypes;
using Helpers.ReflectionHelpers;
using Helpers.ThemeHelper;
using MercadoCinotam.MainPageContent.Admin;
using MercadoCinotam.PymeInfo.Dtos;
using MercadoCinotam.PymeInfo.PymeAdminService;
using MercadoCinotam.ThemeService.Admin;
using MercadoCinotam.Web.Attributes;
using MercadoCinotam.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    public class MyStoreController : MercadoCinotamControllerBase
    {
        private readonly IPymeAdminService _pymeAdminService;
        private readonly IThemeAdminService _themeAdminService;
        private readonly IMainContentAdminService _mainContentAdminService;
        public MyStoreController(IPymeAdminService pymeAdminService, IThemeAdminService themeAdminService, IMainContentAdminService mainContentAdminService)
        {
            _pymeAdminService = pymeAdminService;
            _themeAdminService = themeAdminService;
            _mainContentAdminService = mainContentAdminService;
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

        public ActionResult ContactInfo()
        {
            var contactInfo = _pymeAdminService.GetContactInfoForEdit();
            return View(contactInfo);
        }

        public async Task<ActionResult> Themes()
        {
            var themes = await _themeAdminService.GetThemesForSelector();
            ThemeHelper.ClearTheme(System.Web.HttpContext.Current);
            return View(themes);
        }

        public ActionResult MainPageContents()
        {
            return View();
        }
        [WrapResult(false)]
        public JsonResult GetMainContents(RequestModel input)
        {
            ProccessQueryData(input, "Key", new[] { "Id", "Key", "Value", "ThemeReferenceName" });
            var data = _pymeAdminService.GetMainPageContents(input);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [ShouldBeAjaxRequest]
        public ActionResult EditContent(int? id)
        {
            var content = _mainContentAdminService.GetContentForEdit(id);
            return View(content);
        }
    }
}