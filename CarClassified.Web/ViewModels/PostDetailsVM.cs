using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.ViewModels
{
    public class PostDetailsVM : PosterVM
    {
        public int BodyStyleId { get; set; }
        public int ColorId { get; set; }
        public int ConditionId { get; set; }
        public int CylinderId { get; set; }
        public string Details { get; set; }
        public int FuelId { get; set; }
        public byte[] ImageOne { get; set; }
        public string Location { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public string Title { get; set; }
        public int TransmissionId { get; set; }
    }
}