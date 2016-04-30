using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Views
{
    /// <summary>
    /// Holds listing details
    /// </summary>
    /// <seealso cref="CarClassified.Models.Views.Listings" />
    public class ListingDetail : Listings
    {
        public string Body { get; set; }
        public string BodyStyle { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public int Cylinder { get; set; }
        public string FirstName { get; set; }
        public string Fuel { get; set; }
        public ICollection<Image> Images { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Transmission { get; set; }
    }
}