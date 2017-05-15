using mynb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mynb.Controllers
{
    public class adminController : Controller
    {
        private MySQL sql;
        private User user;
        public bool isChecker = false;

        public adminController()
        {
            sql = new MySQL();
            user = new User(sql);
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View(user);
        }
        [HttpPost]
        public ActionResult Index(User post)
        {
            user.login = post.login;
            user.passw = post.passw;
            user.CheckLogin();
            if (user.status == "1")
                Session["admin"] = "1";

            return View(user);
        }
        public ActionResult Logout()
        {
            Session["admin"] = "";
            //return Redirect("~/admin/index");
            return View("index", user);
        }
        public ActionResult Checker()
        {
            if (Session["admin"] != "1")
                return Redirect("index");

            Story story = new Story(sql);
            isChecker = story.SelectWaitStory();

            // to do select first unapproved story
            return View(story);
        }

        public ActionResult Approve()
        {
            if (Session["admin"] != "1")
                return Redirect("index");

            string id = (RouteData.Values["id"] ?? "").ToString();
            Story story = new Story(sql);
            story.Approve(id);
            return Redirect("~/admin/checker");
        }
        public ActionResult Decline()
        {
            if (Session["admin"] != "1")
                return Redirect("index");

            string id = (RouteData.Values["id"] ?? "").ToString();
            Story story = new Story(sql);
            story.Decline(id);
            return Redirect("~/admin/checker");
        }

    }
}