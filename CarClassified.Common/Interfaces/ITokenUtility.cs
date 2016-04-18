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
        string GenerateToken(string email);

        string GetEmail(string token);

        IDictionary<string, string> ReadToken(string token);
    }
}