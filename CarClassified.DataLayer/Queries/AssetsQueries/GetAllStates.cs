using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.AssetsQueries
{
    /// <summary>
    /// Retrieves all states
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.IQuery{System.Collections.Generic.ICollection{CarClassified.Models.Tables.State}}" />
    public class GetAllStates : IQuery<ICollection<State>>
    {
        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public ICollection<State> Execute(IUnitOfWork unit)
        {
            return unit.Query<State>("SELECT * FROM State").ToList();
        }
    }
}
