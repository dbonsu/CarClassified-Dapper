using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        public PostController()
        {
        }

        [Route("assets")]
        [HttpGet]
        public IEnumerable<object> GetAssets()
        {
            return null;
        }
    }
}
