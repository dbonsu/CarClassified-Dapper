using System.Configuration;

namespace CarClassified.Common
{
    /// <summary>
    /// Common project settings
    /// </summary>
    public static class BaseSettings
    {
        /// <summary>
        /// Gets the audience identifier.
        /// </summary>
        /// <value>
        /// The audience identifier.
        /// </value>
        public static string AudienceId
        {
            get
            {
                return ConfigurationManager.AppSettings["AudienceId"];
            }
        }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["BaseUrl"];
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CarClassifiedDb"].ConnectionString;
            }
        }

        /// <summary>
        /// Gets the email verification URL.
        /// </summary>
        /// <value>
        /// The email verification URL.
        /// </value>
        public static string EmailVerificationUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailVerificationUrl"];
            }
        }

        /// <summary>
        /// Gets the symmetric key.
        /// </summary>
        /// <value>
        /// The symmetric key.
        /// </value>
        public static string SymmetricKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SymmetricKey"];
            }
        }
    }
}
