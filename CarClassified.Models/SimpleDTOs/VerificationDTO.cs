using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Models.SimpleDTOs
{
    public class VerificationDTO
    {
        public string Email { get; set; }

        public bool IsVerified { get; set; }
    }
}