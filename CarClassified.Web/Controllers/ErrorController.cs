using CarClassified.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    /// <summary>
    /// Error views controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ErrorController : Controller
    {
        /// <summary>
        /// Invalids the token.
        /// </summary>
        /// <returns></returns>
        public ActionResult InvalidToken()
        {
            return View();
        }

        /// <summary>
        /// Limits the post.
        /// </summary>
        /// <returns></returns>
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
