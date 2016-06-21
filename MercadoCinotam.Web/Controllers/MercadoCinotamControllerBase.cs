using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.ThemeService;
using Microsoft.AspNet.Identity;
using PymeTamThemeEngine;
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
        public ITenantAppService TenantAppService { get; set; }
        public IThemeService ThemeService { get; set; }
        protected MercadoCinotamControllerBase()
        {
            LocalizationSourceName = MercadoCinotamConsts.LocalizationSourceName;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            InsertViewEngine();
        }

        private void InsertViewEngine()
        {
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

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}