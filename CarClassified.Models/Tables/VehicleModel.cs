using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class VehicleModel : BaseModels.BaseModel
    {
        public int MakeId { get; set; }
        public string Code { get; set; }
    }
}
