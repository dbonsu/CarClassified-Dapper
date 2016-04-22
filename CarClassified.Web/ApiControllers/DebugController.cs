using CarClassified.Common.Interfaces;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.DataLayer.Queries.PostingQueries;
using CarClassified.Models.SimpleDTOs;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/debug")]
    public class DebugController : ApiController
    {
        private IDatabase _db;
        private IUnitOfWork _unit;
        private IVeryBasicEmail _email;
        private ITokenUtility _token;

        public DebugController(IDatabase db, IUnitOfWork unit, IVeryBasicEmail email, ITokenUtility token)
        {
            _db = db;
            _unit = unit;
            _email = email;
            _token = token;
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

        [HttpPost]
        [Route("email")]
        public void SendEmail(string email)
        {
            var url = HttpUtility.UrlEncode("http://localhost:58604/api/email?verify=" + "2jcn9w82fn9wef9ncdscs98cdcs9c");

            _email.SendEmail(email, url);
        }
    }
}
