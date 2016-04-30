using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    public class GetListingDetails : IQuery<ListingDetail>
    {
        private long _postId;

        public GetListingDetails(long postId)
        {
            _postId = postId;
        }

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