using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using SendGrid;
using System;
using System.Net;
using System.Net.Mail;

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
        private static string USERNAME = System.Environment.GetEnvironmentVariable("SENDGRID_USER");
        private static string PASSWORD = System.Environment.GetEnvironmentVariable("SENDGRID_PASS");

        public void SendContact(ContactVM buyer, Contact seller)
        {
            var body = "<div><p>Dear {0} :</p> </div>" +
                     "<div> <p> {1} with email {2} is interested in your car.</p> </div>";

            var message = new SendGridMessage();
            message.AddTo(seller.Email);

            message.Subject = SUBJECT;
            message.Html = string.Format(body, seller.FirstName.Trim() + " " + seller.LastName.Trim(), buyer.Name, buyer.Email);
            var credentials = new NetworkCredential(USERNAME, PASSWORD);
            var transportWeb = new SendGrid.Web(credentials);
            try
            {
                transportWeb.DeliverAsync(message);
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
            var message = new SendGridMessage();
            message.AddTo(emailAddress);

            message.Subject = SUBJECT;
            message.Html = string.Format(body, url);

            try
            {
                var credentials = new NetworkCredential(USERNAME, PASSWORD);
                var transportWeb = new SendGrid.Web(credentials);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
