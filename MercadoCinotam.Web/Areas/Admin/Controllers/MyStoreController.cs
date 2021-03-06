﻿using Abp.UI;
using Abp.Web.Models;
using Helpers.GenericTypes;
using Helpers.ReflectionHelpers;
using MercadoCinotam.MainPageContent.Admin;
using MercadoCinotam.MainPageContent.Dtos;
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

            return View(themes);
        }

        public ActionResult MainPageContents()
        {
            return View();
        }
        [WrapResult(false)]
        public async Task<JsonResult> GetMainContents(RequestModel input)
        {
            var onlyActiveTheme = bool.Parse(input.GenericProperty[0]);
            ProccessQueryData(input, "Key", new[] { "Id", "Key", "Value", "Type", "ThemeReferenceName" });
            var data = await _pymeAdminService.GetMainPageContents(input, onlyActiveTheme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [ShouldBeAjaxRequest]
        public ActionResult EditContent(int? id)
        {
            var content = _mainContentAdminService.GetContentForEdit(id);
            return View(content);
        }
        [HttpPost]
        public async Task<ActionResult> AddEditContent()
        {
            var model = InputBuilder.BuildInputByRequest<ContentInput>(Request);
            if (Request.Files.Count <= 0)
            {
                throw new UserFriendlyException("No hay imagen");
            }
            model.File = Request.Files[0];
            var id = await _mainContentAdminService.AddEditContentWithFile(model);
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> EditHtml()
        {
            var themeContents = await _themeAdminService.GetThemeContentForEdit();
            return View(themeContents);
        }
    }
}