using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to a vehicle listing
    /// </summary>
    public class Vehicle
    {
        public int BodyId { get; set; }

        public int ColorId { get; set; }

        public int ConditionId { get; set; }

        public int CylinderId { get; set; }

        public int FuelId { get; set; }

        public int Id { get; set; }

        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public long PostId { get; set; }
        public int TransmissionId { get; set; }

        /// <summary>
        /// Gets or sets the year. No need for date time object
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        public long Miles { get; set; }
    }
}
