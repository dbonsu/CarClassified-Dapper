using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        public EmailController()
        {
        }

        [HttpGet]
        [Route("token")]
        public IHttpActionResult VerifyEmail(string token)
        {
            return Ok();
        }
    }
}
