using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
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
        private const string CONTACT_SUBJECT = "Car Classified Email Contact";
        private const string SUBJECT = "Car Classified Email Verification";

        public void SendContact(ContactVM buyer, Contact seller)
        {
            var body = "<div><p>Dear {0} :</p> </div>" +
                     "<div> <p> {1} with email {2} is interested in your car.</p> </div>";

            var message = new MailMessage();
            message.To.Add(new MailAddress(seller.Email));

            message.Subject = SUBJECT;
            message.Body = string.Format(body, seller.FirstName.Trim() + " " + seller.LastName.Trim(), buyer.Name, buyer.Email);
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

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="url">The URL.</param>
        public void SendRegistrationEmail(string emailAddress, string url)
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