﻿using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Views;
using System.Collections.Generic;
using System.Linq;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    /// <summary>
    /// Gets active listings
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{System.Collections.Generic.ICollection{CarClassified.Models.Views.Listings}}" />
    public class GetListings : IQuery<ICollection<Listings>>
    {
        private int _stateId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListings"/> class.
        /// </summary>
        public GetListings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListings"/> class.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        public GetListings(int stateId)
        {
            _stateId = stateId;
        }

        public ICollection<Listings> Execute(IUnitOfWork unit)
        {
            string sqlTop20 = @"SELECT TOP 20 p.Id, p.PostDate, p.Title, p.Price,
                                v.Year, v.Make, V.Model, v.Miles from Post p
                                JOIN Vehicle v ON v.PostId = p.Id
                                WHERE p.IsActive =1 ORDER BY p.PostDate DESC";

            string queryWithState = @"SELECT p.Id, p.PostDate, p.Title, p.Price,
                                    v.Year, v.Make, V.Model, v.Miles from Post p
                                    JOIN Vehicle v ON v.PostId = p.Id
                                    JOIN Poster po ON po.Id = p.PosterId
                                    JOIN State s ON s.Id = po.StateId
                                    WHERE p.IsActive =1 AND s.Id =@sId ";

            return _stateId > 0 ? unit.Query<Listings>(queryWithState, new { sId = _stateId }).ToList() : unit.Query<Listings>(sqlTop20).ToList();
        }
    }
}
