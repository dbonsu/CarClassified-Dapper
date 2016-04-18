using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Common
{
    public static class BaseSettings
    {
        private static string _connectionString;

        public static string AudienceId
        {
            get
            {
                return ConfigurationManager.AppSettings["AudienceId"];
            }
        }

        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["BaseUrl"];
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["CarClassifiedDb"].ConnectionString;
                }
                return _connectionString;
            }
        }

        public static string EmailVerificationUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailVerificationUrl"];
            }
        }

        public static string SymmetricKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SymmetricKey"];
            }
        }
    }
}