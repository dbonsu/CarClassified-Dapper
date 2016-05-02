using System;

namespace CarClassified.Models.Views
{
    /// <summary>
    /// Maps to a post(id,title,location,postdate,price) and vehicle (make,model,miles)
    /// </summary>
    public class Listings
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Make { get; set; }
        public long Miles { get; set; }
        public string Model { get; set; }
        public DateTime PostDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
