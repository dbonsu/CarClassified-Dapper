using CarClassified.Models.Tables;
using System.Collections.Generic;

namespace CarClassified.Models.Views
{
    /// <summary>
    /// Maps to all listing items for drop down
    /// </summary>
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
