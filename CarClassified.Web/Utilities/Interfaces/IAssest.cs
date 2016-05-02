using System.Collections.Generic;
using System.Web.Mvc;

namespace CarClassified.Web.Utilities.Interfaces
{
    /// <summary>
    /// Retrieves assests(states, colors etc)
    /// </summary>
    public interface IAssest
    {
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetStates();
    }
}
