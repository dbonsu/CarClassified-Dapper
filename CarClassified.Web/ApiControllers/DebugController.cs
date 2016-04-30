using AutoMapper;
using CarClassified.Common.Interfaces;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.DataLayer.Queries.ListingQueries;
using CarClassified.DataLayer.Queries.PostingQueries;

using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/debug")]
    public class DebugController : ApiController
    {
        private IDatabase _db;
        private IVeryBasicEmail _email;
        private ITokenUtility _token;
        private IUnitOfWork _unit;
        private IMapper _mapper;

        public DebugController(IDatabase db, IUnitOfWork unit, IVeryBasicEmail email, ITokenUtility token, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _email = email;
            _token = token;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("colors")]
        public IHttpActionResult GetColors()
        {
            //var result = _db.Query<ICollection<Color>>(new DebugQuery());
            //if (result.Count > 0)
            //{
            //    return Ok(result);
            //}
            var v = _db.Query<Poster>(new GetPoster("derick@d.com"));
            return BadRequest();
        }

        [HttpGet]
        [Route("")]
        public AllAssests GetStuff()
        {
            var result = _db.Query(new GetAllAssests());
            return result;
        }

        [HttpGet]
        [Route("temp")]
        public object GetToken()
        {
            return _token.GenerateToken("der@d.com");
        }

        [HttpPost]
        [Route("image")]
        public IHttpActionResult ImageTest()
        {
            var files = HttpContext.Current.Request.Files;
            try
            {
                // get variables first
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                var model = new PostDetailsVM();

                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    PropertyInfo pi = model.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        pi.SetValue(model, nvc[kvp], null);
                    }
                }

                var image = HttpContext.Current.Request.Files["Image"];
            }
            catch (Exception ex)
            {
            }
            return Ok();
        }

        [HttpPost]
        [Route("email")]
        public void SendEmail(string email)
        {
            var url = HttpUtility.UrlEncode("http://localhost:58604/api/email?verify=" + "2jcn9w82fn9wef9ncdscs98cdcs9c");

            _email.SendRegistrationEmail(email, url);
        }

        [HttpGet]
        [Route("images")]
        public ListingDetailsVM Getimages()
        {
            var result = _db.Query(new GetListingDetails(7));
            return _mapper.Map<ListingDetailsVM>(result);
        }
    }
}
