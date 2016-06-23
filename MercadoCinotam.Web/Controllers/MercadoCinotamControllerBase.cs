using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Helpers.GenericTypes;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.ThemeService;
using Microsoft.AspNet.Identity;
using PymeTamThemeEngine;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class MercadoCinotamControllerBase : AbpController
    {
        private const string KeySession = "Theme";
        private const string LastTenant = "LastTenant";
        private const string StorePage = "Store";
        public ITenantAppService TenantAppService { get; set; }
        public IThemeService ThemeService { get; set; }
        protected MercadoCinotamControllerBase()
        {
            LocalizationSourceName = MercadoCinotamConsts.LocalizationSourceName;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.TenantName = GetTenancyNameByUrl();
            InsertViewEngine(filterContext);
        }

        private void InsertViewEngine(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName != StorePage)
            {
                return;
            }
            //Clear the view engine
            ClearViewEngine();
            string activeThemeName;
            if (Session[KeySession] == null)
            {
                Session[LastTenant] = TenantName;
                var theme = ThemeService.GetActiveThemeFromTenant(TenantName);
                if (string.IsNullOrEmpty(theme))
                {
                    Session[KeySession] = "";
                    return;
                }
                activeThemeName = theme;
                Session[KeySession] = activeThemeName;
            }
            else
            {
                if ((string)Session[LastTenant] != TenantName)
                {
                    ClearViewEngine();
                    var theme = ThemeService.GetActiveThemeFromTenant(TenantName);
                    if (string.IsNullOrEmpty(theme))
                    {
                        Session[KeySession] = "";
                        return;
                    }
                    activeThemeName = theme;
                    Session[KeySession] = activeThemeName;
                }
                else
                {
                    //we are in the current tenant we jus load the theme from memory
                    activeThemeName = (string)Session[KeySession];
                }
            }
            if (!string.IsNullOrEmpty(activeThemeName))
            {
                ViewEngines.Engines.Insert(0, new ThemeEngine(activeThemeName));
            }
        }

        private static void ClearViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Insert(0, new ThemeEngine.DefaultViewEngine());
        }
        private string GetTenancyNameByUrl()
        {
            if (Request.Url == null) return string.Empty;
            var tenancyName = Request.Url.AbsoluteUri.Split(".").First();
            tenancyName = tenancyName.Split("//").Last();
            return tenancyName;
        }
        public string TenantName => GetTenancyNameByUrl();
        public bool IsHostSite
        {
            get
            {
                var name = GetTenancyNameByUrl();
                var instance = AsyncHelper.RunSync(() => TenantAppService.GetTenantIdByName(name));
                return instance == null;
            }
        }
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }
        /// <summary>
        /// Build the model for the datatables.js request
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="propToSearch">Prop used to filter data if empty tries to search in all props of the object</param>
        /// <param name="reflectedProps">Columns of the table, they need to be in order</param>
        protected void ProccessQueryData(RequestModel requestModel, string propToSearch, string[] reflectedProps)
        {
            if (
                Request.QueryString["order[0][column]"] != null)
            {
                requestModel.PropSort = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                requestModel.PropOrd = Request.QueryString["order[0][dir]"];
            }

            if (!string.IsNullOrEmpty(propToSearch)) requestModel.PropToSearch = propToSearch;
            try
            {
                requestModel.PropToSort = reflectedProps[requestModel.PropSort];
            }
            catch
            {
                throw new Exception("Rango de propiedades invalido.");
            }
        }
        protected void ProccessQueryData(RequestModel requestModel, string[] propsToSearch, string[] reflectedProps)
        {
            if (
                Request.QueryString["order[0][column]"] != null)
            {
                requestModel.PropSort = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                requestModel.PropOrd = Request.QueryString["order[0][dir]"];
            }

            if (propsToSearch.Any()) requestModel.PropsToSearch = propsToSearch;
            try
            {
                requestModel.PropToSort = reflectedProps[requestModel.PropSort];
            }
            catch
            {
                throw new Exception("Rango de propiedades invalido.");
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}