﻿namespace CarClassified.Web.ViewModels
{
    /// <summary>
    /// Maps to posting and editing views
    /// </summary>
    /// <seealso cref="CarClassified.Web.ViewModels.PosterVM" />
    public class PostDetailsVM : PosterVM
    {
        public int BodyStyleId { get; set; }
        public int ColorId { get; set; }
        public int ConditionId { get; set; }
        public int CylinderId { get; set; }
        public string Details { get; set; }
        public int FuelId { get; set; }

        public string Location { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public string Title { get; set; }
        public int TransmissionId { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public long Miles { get; set; }
    }
}
