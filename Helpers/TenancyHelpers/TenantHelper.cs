using System.Web;

namespace Helpers.TenancyHelpers
{
    /// <summary>
    /// Las empresas son resueltas por abp mediante la clase abpsession el proposito de esta clase
    /// es crear una alternativa para cargar el id del tenant sin que el usuario tenga que estar registrado
    /// </summary>
    public static class TenantHelper
    {
        public const string CurrentTenant = "CurrentTenant";

        public static void SetTenant(int? id)
        {
            HttpContext.Current.Session[CurrentTenant] = id;
        }

        public static int? TenantId => (int?)HttpContext.Current.Session[CurrentTenant];
    }
}
