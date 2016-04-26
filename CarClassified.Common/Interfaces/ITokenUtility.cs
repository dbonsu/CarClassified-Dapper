using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Common.Interfaces
{
    /// <summary>
    /// Interface for creating and decoding token
    /// </summary>
    public interface ITokenUtility
    {
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        string GenerateToken(string email);

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        string GetEmail(string token);

        /// <summary>
        /// Reads the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        IDictionary<string, string> ReadToken(string token);
    }
}
