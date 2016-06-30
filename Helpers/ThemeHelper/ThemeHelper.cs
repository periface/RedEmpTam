using System.Web;
using System.Web.SessionState;

namespace Helpers.ThemeHelper
{
    public class ThemeHelper : IHttpHandler, IReadOnlySessionState
    {
        public ThemeHelper(bool isReusable)
        {
            IsReusable = isReusable;
        }

        private const string KeySession = "Theme";
        private const string LastTenant = "LastTenant";
        public static void ClearTheme(HttpContext httpContext)
        {
            if (httpContext.Session != null)
            {
                httpContext.Session[KeySession] = null;
                httpContext.Session[LastTenant] = null;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            return;
        }

        public bool IsReusable { get; }
    }
}
