namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to a vehicle listing
    /// </summary>
    public class Vehicle
    {
        public string BodyStyle { get; set; }

        public string Color { get; set; }

        public string Condition { get; set; }

        public int Cylinder { get; set; }

        public string Fuel { get; set; }

        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public long PostId { get; set; }
        public string Transmission { get; set; }

        /// <summary>
        /// Gets or sets the year. No need for date time object
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        public long Miles { get; set; }
    }
}
