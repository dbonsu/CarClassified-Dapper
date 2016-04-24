using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class Post
    {
        public string Body { get; set; }
        public long Id { get; set; }

        public bool IsActive { get; set; }
        public string Location { get; set; }
        public DateTime PostDate { get; set; }
        public Guid PosterId { get; set; }
        public int StateId { get; set; }
        public string Title { get; set; }
    }
}