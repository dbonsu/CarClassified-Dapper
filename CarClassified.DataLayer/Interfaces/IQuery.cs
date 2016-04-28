using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Interfaces
{
    /// <summary>
    /// Db queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T>
    {
        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        T Execute(IUnitOfWork unit);
    }
}
