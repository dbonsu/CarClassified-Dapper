using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Views
{
    public class Listings
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime PostDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
