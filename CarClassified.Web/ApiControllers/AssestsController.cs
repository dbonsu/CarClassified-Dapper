using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.Models.Views;
using System.Web.Http;

namespace CarClassified.Web.ApiControllers
{
    /// <summary>
    /// Api for all auxillary objects - colors, transmission type etc
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/assests")]
    public class AssestsController : ApiController
    {
        private readonly IDatabase _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssestsController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public AssestsController(IDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all assests.
        /// </summary>
        /// <returns></returns>
        public AllAssests GetAllAssests()
        {
            return _db.Query(new GetAllAssests());
        }
    }
}
