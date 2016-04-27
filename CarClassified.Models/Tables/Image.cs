using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
