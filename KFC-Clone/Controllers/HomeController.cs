using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFC_Clone.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home

        [AllowAnonymous]
        public ActionResult UserForShareView()
        {
            return View();
        }
    }
}