using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries;
using CarClassified.Models.Tables;
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

        public DebugController(IDatabase db, IUnitOfWork unit, IVeryBasicEmail email)
        {
            _db = db;
            _unit = unit;
            _email = email;
        }

        [HttpGet]
        [Route("colors")]
        public IHttpActionResult GetColors()
        {
            var result = _db.Query<ICollection<Color>>(new DebugQuery());
            if (result.Count > 0)
            {
                return Ok(result);
            }

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
