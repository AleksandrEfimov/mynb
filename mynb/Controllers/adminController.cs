using mynb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// adminController allows to edit sent posts
// to do: select first unapproved story
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

        // getting view for admin
        [HttpGet]
        public ActionResult Index()
        {
            return View(user);
        }
        // show View(user) for get page for login and 
        // sending data for authorization
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
        // logout admin account with redirect to "admin/index"
        public ActionResult Logout()
        {
            // set status "admin" equal ""
            Session["admin"] = ""; 
            //return Redirect("~/admin/index");
            return View("index", user);
        }
        // checking received story
        public ActionResult Checker()
        {   
            // foolproof
            if (Session["admin"] != "1")
                return Redirect("index");

            Story story = new Story(sql);
            // to get the sample story with status "wait"
            isChecker = story.SelectWaitStory();

            // WARNING! to do: select first unapproved story
            return View(story);
        }
        // assignment status of "show"
        public ActionResult Approve()
        {   
            if (Session["admin"] != "1")
                return Redirect("index");

            string id = (RouteData.Values["id"] ?? "").ToString();
            Story story = new Story(sql);

            story.Approve(id);
            return Redirect("~/admin/checker");
        }

        // rejected story is not show to users
        public ActionResult Decline()
        {
            if (Session["admin"] != "1")
                return Redirect("index");

            string id = (RouteData.Values["id"] ?? "").ToString();
            Story story = new Story(sql);
            story.Decline(id);
            // return checking page
            return Redirect("~/admin/checker");
        }

    }
}