using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Views
{
    public class AllAssests
    {
        public ICollection<BodyStyle> BodyStyles { get; set; }
        public ICollection<Color> Colors { get; set; }
        public ICollection<Condition> Conditions { get; set; }
        public ICollection<Cylinder> Cylinders { get; set; }
        public ICollection<Fuel> FuelTypes { get; set; }
        public ICollection<Make> Makes { get; set; }
        public ICollection<Transmission> Transmissions { get; set; }
    }
}