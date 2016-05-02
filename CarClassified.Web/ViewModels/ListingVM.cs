namespace CarClassified.Web.ViewModels
{
    /// <summary>
    /// Holds listing (post and vehicle)
    /// </summary>
    public class ListingVM
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public long Miles { get; set; }
        public string Model { get; set; }
        public string PostDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
