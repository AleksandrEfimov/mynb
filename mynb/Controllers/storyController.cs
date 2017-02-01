using mynb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mynb.Controllers
{
    public class storyController : Controller
    {
        // GET: story
        public ActionResult Index()
        {
            return View(); // "random"
        }


        public ActionResult random()
        {
            Story story = new Story();
            story.Random();
            return View("number", story);
        }

        public ActionResult add()
        {
            return View();
        }

        // показывает одну историю/уже две
        public ActionResult number()
        {
            Story story = new Story();
            return View(story);
        }
    }
}