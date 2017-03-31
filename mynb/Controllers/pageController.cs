using mynb.Models;
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
            Story story = new Story();
            story.GenerateList("4");
            return View(story);
        }
    }
}