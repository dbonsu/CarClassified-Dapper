using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/assests")]
    public class AssestsController : ApiController
    {
        private IDatabase _db;

        public AssestsController(IDatabase db)
        {
            _db = db;
        }

        public AllAssests GetAllAssests()
        {
            return _db.Query(new GetAllAssests());
        }
    }
}
