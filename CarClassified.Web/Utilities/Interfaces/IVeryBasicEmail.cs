using CarClassified.Models.Views;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Web.Utilities.Interfaces
{
    /// <summary>
    /// Simple email
    /// </summary>
    public interface IVeryBasicEmail
    {
        /// <summary>
        /// Sends the contact.
        /// </summary>
        /// <param name="buyer">The buyer.</param>
        /// <param name="seller">The seller.</param>
        void SendContact(ContactVM buyer, Contact seller);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="url">The URL.</param>
        void SendRegistrationEmail(string emailAddress, string url);
    }
}