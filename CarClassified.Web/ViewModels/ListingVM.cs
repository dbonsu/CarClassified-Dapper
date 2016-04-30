using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.ViewModels
{
    public class ListingVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string PostDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public long Miles { get; set; }
        public decimal Price { get; set; }
    }
}
