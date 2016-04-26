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
        /// Sends the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="url">The URL.</param>
        void SendEmail(string emailAddress, string url);
    }
}
