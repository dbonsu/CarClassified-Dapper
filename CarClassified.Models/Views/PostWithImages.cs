using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Views
{
    public class PostWithImages
    {
        public Post Post { get; set; }
        public Poster Poster { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
