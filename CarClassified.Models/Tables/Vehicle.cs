using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class Vehicle
    {
        //TODO:remove location from db
        public int Id { get; set; }

        public int ImageOne { get; set; }
        public int ColorId { get; set; }
        public int TransmissionId { get; set; }
        public int BodyId { get; set; }
        public int ConditionId { get; set; }
        public int FuelId { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int ImageTwo { get; set; }
        public int CylinderId { get; set; }
        public int PostId { get; set; }
    }
}
