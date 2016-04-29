using AutoMapper;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.ListingQueries;
using CarClassified.Models.Views;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    [RoutePrefix("api/listings")]
    public class ListingController : ApiController
    {
        private IDatabase _db;
        private IMapper _mapper;

        public ListingController(IDatabase db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<ListingVM> GetListings()
        {
            var listings = _db.Query(new GetListings());
            return _mapper.Map<IEnumerable<ListingVM>>(listings);
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<ListingVM> GetListings(int stateId)
        {
            var listings = _db.Query(new GetListings(stateId));
            return _mapper.Map<IEnumerable<ListingVM>>(listings);
        }

        [Route("details")]
        [HttpGet]
        public ListingDetailsVM GetListingDetails(long Id)
        {
            var result = _db.Query(new GetListingDetails(Id));
            return _mapper.Map<ListingDetailsVM>(result);
        }
    }
}
