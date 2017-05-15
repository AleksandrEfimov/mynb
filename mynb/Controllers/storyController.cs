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
        private Story story, post;
        private MySQL sql;

        public storyController()
            {
                sql = new MySQL();
                story = new Story(sql);
                // тестируем инициализацию Story post
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

        [HttpGet]
        public ActionResult add()
        {
            story.title = "";
            story.story = "";
            story.email = "";
           
            if (IsError()) return ErrorActionResult;
            return View(story);
        }
        [HttpPost]
        public ActionResult add(Story post )
        {
            
            if (!ModelState.IsValid)
                return View(post);

            //story = post;
            story.title = post.title;
            story.story = post.story;
            story.email = post.email;
            story.Add();
            if (IsError()) return ErrorActionResult;
            return Redirect("/story/number/"+story.id);    
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
            if (sql.IsError())
            {
                ViewBag.error = sql.error;
                ViewBag.query = sql.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            if (story.IsError())
            {
                ViewBag.error = story.error;
                ViewBag.query = sql.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            return false;
        }

    }
}