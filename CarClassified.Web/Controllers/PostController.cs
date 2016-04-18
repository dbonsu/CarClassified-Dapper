using CarClassified.Models.Tables;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Create()
        {
            // ViewBag.states = GetStates();
            var posterVM = new PosterVM
            {
                UserStates = GetStates()
            };
            return View(posterVM);
        }

        [HttpPost]
        public ActionResult Create(PosterVM post)
        {
            return RedirectToAction("Index");
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Email(string token)
        {
            //read token and get emailaddress
            //user has not already be verified and exists

            // if ()
            //{
            TempData["validuser"] = new PosterVM { };
            return RedirectToAction("Complete");
            //}else(){
            //send user to error page with decription
            //return RedirectToAction("InvalidToken", "Error");
            //}
        }

        public ActionResult Complete()
        {
            var poster = TempData["validuser"] as PosterVM;
            //open with with default data and partial of other values to add
            return View(poster);
        }

        private IEnumerable<SelectListItem> GetStates()
        {
            ICollection<State> states = new List<State>
            {
                new State {Id = 1, Code="AL", Name="Alabama" },
                new State {Id =2, Code="AZ", Name="Arizona" }
            };

            var result = states.Select(x =>
           new SelectListItem
           {
               Value = x.Id.ToString(),
               Text = x.Code
           });

            return new SelectList(states, "Id", "Code");
        }
    }
}
