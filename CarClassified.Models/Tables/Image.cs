namespace CarClassified.Models.Tables
{
    /// <summary>
    /// Maps to image in db
    /// </summary>
    public class Image
    {
        public int Id { get; set; }
        public byte[] Body { get; set; }
        public string Extension { get; set; }
    }
}
