namespace CarClassified.Web.ViewModels
{
    /// <summary>
    /// Maps to posting and editing views
    /// </summary>
    /// <seealso cref="CarClassified.Web.ViewModels.PosterVM" />
    public class PostingDetailsVM : PosterVM
    {
        public string BodyStyle { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public int Cylinder { get; set; }
        public string Details { get; set; }
        public string Fuel { get; set; }

        public string Location { get; set; }
        public string Make { get; set; }
        public long Miles { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Transmission { get; set; }
        public int Year { get; set; }
    }
}
