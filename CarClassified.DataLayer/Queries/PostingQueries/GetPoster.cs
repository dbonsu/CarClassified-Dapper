using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.SimpleDTOs;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.PostingQueries
{
    /// <summary>
    /// Retrieve poster(user) object
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{CarClassified.Models.Tables.Poster}" />
    public class GetPoster : IQuery<Poster>
    {
        private string _email;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPoster"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        public GetPoster(string email)
        {
            _email = email;
        }

        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public Poster Execute(IUnitOfWork unit)
        {
            return unit.Query<Poster>("SELECT * FROM Poster WHERE Email=@email", new { email = _email }).FirstOrDefault();
        }
    }
}
