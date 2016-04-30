using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    /// <summary>
    /// Handles listings views
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ListingController : Controller
    {
        private readonly IAssest _assest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListingController"/> class.
        /// </summary>
        /// <param name="assest">The assest.</param>
        public ListingController(IAssest assest)
        {
            _assest = assest;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.states = _assest.GetStates();
            return View();
        }

        /// <summary>
        /// Successes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            return View();
        }
    }
}