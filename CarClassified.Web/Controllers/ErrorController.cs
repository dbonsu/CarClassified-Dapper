using CarClassified.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult InvalidToken()
        {
            return View();
        }

        public ActionResult LimitPost()
        {
            var error = TempData["error"].ToString();

            if (error.Equals(ErrorConstants.ERROR_lIMIT_ONE))
            {
                ViewBag.Header = "You Have Exceeded Your Posting Limits.";
            }
            else
            {
                ViewBag.Header = "Did You Check Your Email?";
            }

            ViewBag.Error = TempData["error"];
            return View();
        }
    }
}
