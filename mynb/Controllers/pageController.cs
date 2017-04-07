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
        private MySQL sql;
        // GET: page
        public ActionResult Index()
        {
            sql = new MySQL();
            Story story = new Story(sql);
            story.GenerateList("10");
            return View(story);
        }
    }
}