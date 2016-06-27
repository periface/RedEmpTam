using DotLiquid;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Controllers
{
    public class HomeController : MercadoCinotamControllerBase
    {


        public ActionResult Index()
        {

            if (!IsHostSite)
            {
                return RedirectToAction("Index", "Landing");
            }
            //if (!IsHostSite)
            //{
            //    return RedirectToAction("LandingPage");
            //}
            //if is trying to access to a main page with a tenancy name we should send the user to the store profile page
            return View();
        }


        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Index(ProductPage input)
        //{
        //    if (input.Html.Contains("<script>"))
        //    {
        //        return View("Error");
        //    }
        //    if (AbpSession.TenantId != null) input.TenantId = (int)AbpSession.TenantId;
        //    _htmlService.SaveHtml(input);
        //    var model = new MyModel(input.Html);
        //    return Json(new { html = model, id = input.Id });
        //}
        public class MyModel : Drop
        {
            public MyModel(string htmlContent)
            {
                var anonObject = Hash.FromAnonymousObject(new
                {
                    VendorInfo = new
                    {
                        Name = "Alan",
                        SurName = "Torres",
                        StoreInfo = new
                        {
                            StoreName = "Cinotam",
                            About = "Samuel L. Jackson is arguably one of the most quotable Hollywood actors of all time. It’s no surprise that a dummy text generator comprised of his quotes made our list. For those who don’t appreciate the abundance of swear words, there is also a lite version with no profanity."
                        }
                    },
                    ProductInfo = new
                    {
                        Name = "Jabon",
                        Id = 1,
                        ProductDescription = "Samuel L. Jackson is arguably one of the most quotable Hollywood actors of all time. It’s no surprise that a dummy text generator comprised of his quotes made our list. For those who don’t appreciate the abundance of swear words, there is also a lite version with no profanity."
                    }
                });
                var parsed = Template.Parse(htmlContent).Render(anonObject);
                Html = parsed;
            }
            public string Html { get; set; }
        }
    }
}