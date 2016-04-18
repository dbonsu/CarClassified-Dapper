using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries;
using CarClassified.DataLayer.Queries.PostingQueries;
using CarClassified.Models.SimpleDTOs;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/debug")]
    public class DebugController : ApiController
    {
        private IDatabase _db;
        private IUnitOfWork _unit;

        public DebugController(IDatabase db, IUnitOfWork unit)
        {
            _db = db;
            _unit = unit;
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
            var v = _db.Query<VerificationDTO>(new GetPosterVerification("derick@d.com"));
            return BadRequest();
        }
    }
}