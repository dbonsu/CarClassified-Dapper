using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarClassified.Web.Utilities.Interfaces
{
    public interface IAssest
    {
        IEnumerable<SelectListItem> GetStates();
    }
}
