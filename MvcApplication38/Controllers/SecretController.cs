using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication38.Controllers
{
    [Authorize(Roles = "admin, super")]
    public class SecretController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
