using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    public class GetContactInfo : IQuery<Contact>
    {
        private long _postId;

        public GetContactInfo(long postId)
        {
            _postId = postId;
        }

        public Contact Execute(IUnitOfWork unit)
        {
            string query = @"SELECT po.Email, po.FirstName, po.LastName FROM Poster po
                            JOIN Post p ON p.PosterId = po.Id
                            WHERE p.Id = @postId";
            return unit.Query<Contact>(query, new { postId = _postId }).SingleOrDefault();
        }
    }
}