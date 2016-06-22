using MercadoCinotam.Web.Controllers;
using System.Web.Mvc;

namespace MercadoCinotam.Web.Areas.Admin.Controllers
{
    public class OrdersController : MercadoCinotamControllerBase
    {
        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}