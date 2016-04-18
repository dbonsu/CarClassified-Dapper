using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class Post
    {
        //change spelling of title in db
        public int Id { get; set; }

        public int Title { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public string Location { get; set; }
        public Guid PosterId { get; set; }
        public int StateId { get; set; }
        public DateTime PostDate { get; set; }
    }
}
