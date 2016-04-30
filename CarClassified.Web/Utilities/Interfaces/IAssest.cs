﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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