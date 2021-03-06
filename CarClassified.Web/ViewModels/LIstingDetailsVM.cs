﻿using CarClassified.Models.Tables;
using System.Collections.Generic;

namespace CarClassified.Web.ViewModels
{
    /// <summary>
    /// Holds listing detail information
    /// </summary>
    /// <seealso cref="CarClassified.Web.ViewModels.ListingVM" />
    public class ListingDetailsVM : ListingVM
    {
        public string Body { get; set; }
        public string BodyStyle { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public int Cylinder { get; set; }
        public string Fuel { get; set; }
        public ICollection<Image> Images { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Transmission { get; set; }
    }
}
