using Abp.UI;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Attributes
{
    public class ShouldBeAjaxRequest : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                throw new UserFriendlyException("Pagina no disponible");
            }
        }
    }
}