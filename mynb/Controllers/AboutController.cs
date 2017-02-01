using mynb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mynb.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            About about = new About();
            return View(about);
        }
    }
}