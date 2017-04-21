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
            return View(user);
        }
    }
}