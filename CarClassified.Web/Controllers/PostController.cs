using AutoMapper;
using CarClassified.Common;
using CarClassified.Common.Constants;
using CarClassified.Common.Interfaces;
using CarClassified.DataLayer.Commands.PostingCommands;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.DataLayer.Queries.PostingQueries;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Controllers
{
    /// <summary>
    /// Post views controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class PostController : Controller
    {
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
            IMapper mapper)
        {
            _db = db;
            _email = email;
            _tokenUtil = tokenUtil;
            _mapper = mapper;
        }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Complete()
        {
            //TODO: remove
            ViewBag.user = new PosterVM
            {
                Email = "der2@d.com",
                FirstName = "first",
                LastName = "last",
                Phone = "555-555-5555",
                Id = new Guid("ec0d371f-721d-498a-9920-13eaa4528629"),
                StateId = 13
            };

            //_sessionUtil.GetPoster(); // TempData["validuser"] as PosterVM;
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
                UserStates = GetStates()
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
                post.UserStates = GetStates();
                return View(post);
            }
            //TODO: check for user email in db
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
            //var identity = new GenericIdentity(user.Email, "Basic");
            //var principal = new GenericPrincipal(identity, new string[] { "validuser" });
            //Thread.CurrentPrincipal = principal;
            //var name = Thread.CurrentPrincipal.Identity.Name;

            //TempData["validuser"] = user;

            //TODO: uncomment

            return RedirectToAction("Complete");
        }

        // GET: Post
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
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

        /// <summary>
        /// Oks this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Ok()
        {
            return View();
        }

        /// <summary>
        /// Images this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Image()
        {
            //_sessionUtil.GetPostWithImages();

            return View();
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetStates()
        {
            ICollection<State> statesdb = _db.Query(new GetAllStates());
            ICollection<StateVM> states = _mapper.Map<ICollection<StateVM>>(statesdb);

            var result = states.Select(x =>
            new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return new SelectList(result, "Value", "Text");
        }

        private void RegisterAndSendEmail(PosterVM post)
        {
            Poster poster = _mapper.Map<Poster>(post);

            _db.Execute(new CreateNewPoster(poster));
            string url = HttpUtility.UrlEncode(BaseSettings.BaseUrl + BaseSettings.EmailVerificationUrl);
            string token = _tokenUtil.GenerateToken(post.Email);
            _email.SendEmail(post.Email, url + token);
        }
    }
}
