using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to vehicle Model
    /// </summary>
    /// <seealso cref="CarClassified.Models.BaseModels.BaseModel" />
    public class Model : BaseModels.BaseModel
    {
        public int MakeId { get; set; }
        public string Code { get; set; }
    }
}
