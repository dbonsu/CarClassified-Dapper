using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace CarClassified.Web.Utilities
{
    /// <summary>
    /// Concrete class for handling simple email
    /// </summary>
    /// <seealso cref="CarClassified.Web.Utilities.Interfaces.IVeryBasicEmail" />
    public class VeryBasicEmail : IVeryBasicEmail

    {
        private const string SUBJECT = "CarClassified Email Verification";

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="url">The URL.</param>
        public void SendEmail(string emailAddress, string url)
        {
            var body = "<div>" +
                            "<p>Please follow the link below to complete your posting.</p>" +
                        "</div>" +
                   "<div><p><a href=\"{0}\">Confirm Your Email</a></p></div";
            var message = new MailMessage();
            message.To.Add(new MailAddress(emailAddress));

            message.Subject = SUBJECT;
            message.Body = string.Format(body, url);
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
