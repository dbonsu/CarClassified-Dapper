using AutoMapper;
using CarClassified.Common;
using CarClassified.Common.Constants;
using CarClassified.Common.Interfaces;
using CarClassified.DataLayer.Commands.PostingCommands;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.DataLayer.Queries.PostingQueries;
using CarClassified.Models.Tables;
using CarClassified.Web.Utilities.Interfaces;
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
        private IDatabase _db;
        private IVeryBasicEmail _email;
        private ITokenUtility _tokenUtil;
        private IMapper _mapper;

        public PostController(IDatabase db, IVeryBasicEmail email, ITokenUtility tokenUtil, IMapper mapper)
        {
            _db = db;
            _email = email;
            _tokenUtil = tokenUtil;
            _mapper = mapper;
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
            var user = _db.Query(new GetPosterVerification(post.Email));
            if (user != null)
            {
                //user cannot not have a user in the system
                if (user.IsVerified)
                {
                    TempData["error"] = ErrorConstants.ERROR_lIMIT_ONE;
                    RedirectToAction("LimitPost", "Error");
                }
                else
                {
                    TempData["error"] = ErrorConstants.ERROR_CHECK_EMAIL;
                    RedirectToAction("LimitPost", "Error");
                }
            }
            //register user and send email
            RegisterAndSendEmail(post);

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

        private void RegisterAndSendEmail(PosterVM post)
        {
            Poster poster = Mapper.Map<Poster>(post);

            _db.Execute(new CreateNewPoster(poster));
            string url = HttpUtility.UrlEncode(BaseSettings.BaseUrl + BaseSettings.EmailVerificationUrl);
            string token = _tokenUtil.GenerateToken(post.Email);
            _email.SendEmail(post.Email, url + token);
        }

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

            return new SelectList(result, "Id", "Name");
        }
    }
}
