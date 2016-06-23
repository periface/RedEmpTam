namespace MercadoCinotam.Web.Models.Account
{
    public class LoginFormViewModel
    {
        public string ReturnUrl { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }
        public bool IsHostSite { get; set; }
        public string TenantName { get; set; }
    }
}