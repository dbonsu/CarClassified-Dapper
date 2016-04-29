using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.ViewModels
{
    public class ListingDetailsVM : ListingVM
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public int Cylinder { get; set; }
        public string BodyStyle { get; set; }
        public string Condition { get; set; }
        public string Fuel { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public string Transmission { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
