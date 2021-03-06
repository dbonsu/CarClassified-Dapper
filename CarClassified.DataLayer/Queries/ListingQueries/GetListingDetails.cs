﻿using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using System.Linq;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    /// <summary>
    /// Gets a detail for a listing
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{CarClassified.Models.Views.ListingDetail}" />
    public class GetListingDetails : IQuery<ListingDetail>
    {
        private long _postId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListingDetails"/> class.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        public GetListingDetails(long postId)
        {
            _postId = postId;
        }

        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public ListingDetail Execute(IUnitOfWork unit)
        {
            string sqlQuery = @"SELECT p.Id, po.FirstName,po.LastName,   p.PostDate, p.Title, p.Price,p.Body,p.Location,
                               v.Year, v.Make, V.Model, v.Miles,v.BodyStyle, v.Color,v.Transmission,v.Cylinder,
                               v.Condition, v.Fuel,s.Name AS State,
                               po.Phone from Post p
                               JOIN Vehicle v ON v.PostId = p.Id
                               JOIN Poster po ON po.Id = p.PosterId
                               JOIN State s ON s.Id = po.StateId
                               WHERE p.IsActive =1 AND p.Id =@Id";

            string sqlImages = @"SELECT i.Body, i.Extension FROM Image i
                               JOIN PostImage p ON p.ImageId = i.Id
                               WHERE p.PostId =@PostId";

            var listing = unit.Query<ListingDetail>(sqlQuery, new { Id = _postId }).FirstOrDefault();
            var images = unit.Query<Image>(sqlImages, new { PostId = _postId }).ToList();
            listing.Images = images;
            return listing;
        }
    }
}
