using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to db
    /// </summary>
    /// <seealso cref="CarClassified.Models.BaseModels.BaseModel" />
    public class Make : BaseModels.BaseModel

    {
        public string Code { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
