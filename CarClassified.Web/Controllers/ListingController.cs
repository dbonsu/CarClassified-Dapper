using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    public class ListingController : Controller
    {
        private readonly IAssest _assest;

        public ListingController(IAssest assest)
        {
            _assest = assest;
        }

        public ActionResult Index()
        {
            ViewBag.states = _assest.GetStates();
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}