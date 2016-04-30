using AutoMapper;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.ListingQueries;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    /// <summary>
    /// Handles all listings
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/listings")]
    public class ListingController : ApiController
    {
        private IDatabase _db;
        private IVeryBasicEmail _email;
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListingController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="email">The email.</param>
        public ListingController(IDatabase db, IMapper mapper, IVeryBasicEmail email)
        {
            _db = db;
            _mapper = mapper;
            _email = email;
        }

        /// <summary>
        /// Contacts the seller.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("contact")]
        [HttpPost]
        public IHttpActionResult ContactSeller(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                Contact contact = _db.Query(new GetContactInfo(model.PostId));
                _email.SendContact(model, contact);
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Gets the listing details.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [Route("details")]
        [HttpGet]
        public ListingDetailsVM GetListingDetails(long Id)
        {
            var result = _db.Query(new GetListingDetails(Id));
            return _mapper.Map<ListingDetailsVM>(result);
        }

        /// <summary>
        /// Gets the listings.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<ListingVM> GetListings()
        {
            var listings = _db.Query(new GetListings());
            return _mapper.Map<IEnumerable<ListingVM>>(listings);
        }

        /// <summary>
        /// Gets the listings.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<ListingVM> GetListings(int stateId)
        {
            var listings = _db.Query(new GetListings(stateId));
            return _mapper.Map<IEnumerable<ListingVM>>(listings);
        }
    }
}