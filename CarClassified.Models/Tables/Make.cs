using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class Make : BaseModels.BaseModel

    {
        public string Code { get; set; }
        public ICollection<VehicleModel> Models { get; set; }
    }
}
