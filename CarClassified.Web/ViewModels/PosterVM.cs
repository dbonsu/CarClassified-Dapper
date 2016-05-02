using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarClassified.Web.ViewModels
{
    /// <summary>
    /// Maps to registring user
    /// </summary>
    public class PosterVM
    {
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        public Guid Id { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DisplayName("State")]
        public int StateId { get; set; }

        public string StateName { get; set; }
        public IEnumerable<SelectListItem> UserStates { get; set; }
    }
}
