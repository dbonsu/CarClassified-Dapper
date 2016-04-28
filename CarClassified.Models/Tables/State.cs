using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to a state object
    /// </summary>
    /// <seealso cref="CarClassified.Models.BaseModels.BaseModel" />
    public class State : BaseModels.BaseModel
    {
        public string Code { get; set; }
    }
}
