using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Interfaces
{
    /// <summary>
    /// Db commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        void Execute(IUnitOfWork unit);
    }
}
