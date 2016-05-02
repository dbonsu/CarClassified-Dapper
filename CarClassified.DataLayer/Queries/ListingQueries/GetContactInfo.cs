using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Views;
using System.Linq;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    /// <summary>
    /// Gets a contact information for a seller
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{CarClassified.Models.Views.Contact}" />
    public class GetContactInfo : IQuery<Contact>
    {
        private long _postId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactInfo"/> class.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        public GetContactInfo(long postId)
        {
            _postId = postId;
        }

        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public Contact Execute(IUnitOfWork unit)
        {
            string query = @"SELECT po.Email, po.FirstName, po.LastName FROM Poster po
                            JOIN Post p ON p.PosterId = po.Id
                            WHERE p.Id = @postId";
            return unit.Query<Contact>(query, new { postId = _postId }).SingleOrDefault();
        }
    }
}
