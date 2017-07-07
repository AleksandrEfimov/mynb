using mynb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// this part contain methods which work with stories 
// todo: what is field "post"? Answer: to pass the value of the argument in method Add(Story post) below.
// m.b. to add try/catch for constructor storyController()?
// todo rename view "number" on "random" method 


namespace mynb.Controllers
{
    public class storyController : Controller

    {
        // ErrorActionResult - return error page
        public ActionResult ErrorActionResult;
        // post?
        private Story story; // post;
        private MySQL sql;

        public storyController()
            {
                // test of initialization connect throw "sql"   
                sql = new MySQL();
                // create connect new users for ability adding story 
                story = new Story(sql);
                
            }

        // if all works without this part after 6 months from the date of 05.07.2017 - to delete.
        // GET: story
        //public ActionResult Index()
        //{
            
        //    if (IsError()) return ErrorActionResult;
        //    return View("number", story);

        //}

        // create page with random story from DB
        public ActionResult random()
        {
           
            story.Random();
            if (IsError()) return ErrorActionResult;
            return View("number", story);
        }

        // initialization void form for adding story 
        [HttpGet]
        public ActionResult add()
        {
            story.title = "";
            story.story = "";
            story.email = "";
            //model.Genres = from genre in Data.GetGenres() 
            //select new SelectListItem {Text = genre.Name, Value = genre.Id.ToString()};
            

           // SelectList categories = new SelectList(Story.Categories);

            if (IsError()) return ErrorActionResult;
            return View(story);
        }
        // after added story getting preview story
        [HttpPost]
        public ActionResult add(Story post)
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

        // select and show story by id
        public ActionResult number()
        {
            string id = (RouteData.Values["id"] ?? "").ToString();
            if (id == "")
                return Redirect("/page");
            story.Number(id);
            if (IsError()) return ErrorActionResult;
            return View(story);
        }



        // catch error state
        // type sql - result is error sql query
        // type story - result is error compleate methods: add, ExtractRaw
        #region
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
        #endregion

    }
}