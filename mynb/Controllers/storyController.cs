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
        private Story story;
        public storyController()
            {
               story = new Story(); 
            }
        // GET: story
        public ActionResult Index()
        {
            
            if (IsError()) return ErrorActionResult;

            return View("number", story);

        }


        public ActionResult random()
        {
           
            story.Random();
            if (IsError()) return ErrorActionResult;
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
            
            string id = (RouteData.Values["id"] ?? "").ToString();
            if (id == "")
                return Redirect("/page");
            story.Number(id);
            if (IsError()) return ErrorActionResult;
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
            if (story.IsError())
            {
                ViewBag.error = "Story not found";
                ViewBag.query = MySQL.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            return false;
        }

    }
}