using AutoMapper;
using CarClassified.Common;
using CarClassified.Common.Constants;
using CarClassified.Common.Interfaces;
using CarClassified.DataLayer.Commands.PostingCommands;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.PostingQueries;
using CarClassified.Models.Tables;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    /// <summary>
    /// Post views controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class PostController : Controller
    {
        private readonly IAssest _assest;
        private IDatabase _db;
        private IVeryBasicEmail _email;
        private IMapper _mapper;
        private ITokenUtility _tokenUtil;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="email">The email.</param>
        /// <param name="tokenUtil">The token utility.</param>
        /// <param name="mapper">The mapper.</param>
        public PostController(IDatabase db, IVeryBasicEmail email, ITokenUtility tokenUtil,
            IMapper mapper, IAssest assest)
        {
            _db = db;
            _email = email;
            _tokenUtil = tokenUtil;
            _mapper = mapper;
            _assest = assest;
        }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Complete()
        {
            ViewBag.user = TempData["validuser"] as PosterVM;

            return View();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var posterVM = new PosterVM
            {
                UserStates = _assest.GetStates()
            };
            return View(posterVM);
        }

        /// <summary>
        /// Creates the specified post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(PosterVM post)
        {
            if (!ModelState.IsValid)
            {
                post.UserStates = _assest.GetStates();
                return View(post);
            }

            Poster poster = _db.Query(new GetPoster(post.Email));
            if (poster != null)
            {
                //user cannot have a user in the system
                if (poster.IsVerified)
                {
                    TempData["error"] = ErrorConstants.ERROR_lIMIT_ONE;
                    return RedirectToAction("LimitPost", "Error");
                }
                else
                {
                    TempData["error"] = ErrorConstants.ERROR_CHECK_EMAIL;
                    return RedirectToAction("LimitPost", "Error");
                }
            }
            //register user and send email
            RegisterAndSendEmail(post);

            return RedirectToAction("Success");
        }

        /// <summary>
        /// Emails the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public ActionResult Email(string token)
        {
            //read token and get emailaddress
            //user has not already been verified and exists
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("InvalidToken", "Error");
            }

            string email = _tokenUtil.GetEmail(token);
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("InvalidToken", "Error");
            }

            var user = _db.Query(new GetPoster(email));
            if (user.IsVerified)
            {
                return RedirectToAction("InvalidToken", "Error");
            }

            //set user to identity or keep temp
            var identity = new GenericIdentity(user.Email, "Basic");
            var principal = new GenericPrincipal(identity, new string[] { "validuser" });
            Thread.CurrentPrincipal = principal;

            TempData["validuser"] = _mapper.Map<PosterVM>(user);

            return RedirectToAction("Complete");
        }

        /// <summary>
        /// Images this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Image()
        {
            return View();
        }

        /// <summary>
        /// Oks this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Ok()
        {
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

        private void RegisterAndSendEmail(PosterVM post)
        {
            Poster poster = _mapper.Map<Poster>(post);

            _db.Execute(new CreateNewPoster(poster));
            string url = BaseSettings.BaseUrl + BaseSettings.EmailVerificationUrl; // HttpUtility.UrlEncode(BaseSettings.BaseUrl + BaseSettings.EmailVerificationUrl);
            string token = _tokenUtil.GenerateToken(post.Email);
            _email.SendRegistrationEmail(post.Email, url + token);
        }
    }
}
