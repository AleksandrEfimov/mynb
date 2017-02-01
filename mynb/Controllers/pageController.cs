using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mynb.Controllers
{
    public class pageController : Controller
    {
        // GET: page
        public ActionResult Index()
        {
            return View();
        }
    }
}