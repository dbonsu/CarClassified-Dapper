using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
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
        public int Year { get; set; }
    }
}
