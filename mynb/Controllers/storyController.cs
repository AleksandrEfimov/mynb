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
        public ActionResult ErrorActionResult;
        // GET: story
        public ActionResult Index()
        {
            Story story = new Story();
            if (IsError()) return ErrorActionResult;

            return View("number", story);

        }


        public ActionResult random()
        {
            Story story = new Story();
            if (IsError()) return ErrorActionResult;
            story.Random();
            return View("number", story);
        }

        public ActionResult add()
        {
            if (IsError()) return ErrorActionResult;
            return View();
        }

        // показывает одну историю/уже две
        public ActionResult number()
        {
            if (IsError()) return ErrorActionResult;
            Story story = new Story();
            return View(story);
        }

        public bool IsError()
        {
            if (MySQL.IsError())
            {
                ViewBag.error = MySQL.error;
                ViewBag.query = MySQL.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            return false;
        }

    }
}