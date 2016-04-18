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
        public PostController()
        {
        }

        public ActionResult Create()
        {
            var posterVM = new PosterVM
            {
                UserStates = GetStates()
            };
            return View(posterVM);
        }

        [HttpPost]
        public ActionResult Create(PosterVM post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            //TODO: check for user email in db

            return RedirectToAction("Index");
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
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