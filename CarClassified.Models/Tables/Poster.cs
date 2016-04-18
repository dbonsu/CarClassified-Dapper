using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.Tables
{
    public class Poster
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public Guid Id { get; set; }
        public bool IsVerified { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int StateId { get; set; }
    }
}