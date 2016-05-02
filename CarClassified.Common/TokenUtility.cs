using CarClassified.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Thinktecture.IdentityModel.Tokens;

namespace CarClassified.Common
{
    /// <summary>
    /// Concrete class for token generation and reading
    /// </summary>
    /// <seealso cref="CarClassified.Common.Interfaces.ITokenUtility" />
    public class TokenUtility : ITokenUtility
    {
        private const string _scheme = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        private string _audienceId = BaseSettings.AudienceId;
        private string _issuer = "self";
        private byte[] _keyByteArray;
        private HmacSigningCredentials _signingKey;
        private string _symmetricKeyAsBase64 = BaseSettings.SymmetricKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenUtility"/> class.
        /// </summary>
        public TokenUtility()
        {
            _keyByteArray = Encoding.Default.GetBytes(_symmetricKeyAsBase64);

            _signingKey = new HmacSigningCredentials(_keyByteArray);
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public string GenerateToken(string email)
        {
            var issued = DateTime.UtcNow;

            var expires = DateTime.UtcNow.AddHours(24);
            var claims = new[]
            {
                new Claim("email",email),
            };

            var token = new JwtSecurityToken(_issuer, _audienceId, claims, issued, expires, _signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public string GetEmail(string token)
        {
            IDictionary<string, string> result = ReadToken(token);
            if (result != null)
            {
                string email;
                result.TryGetValue(_scheme, out email);
                return email;
            }
            return "";
        }

        /// <summary>
        /// Reads the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public IDictionary<string, string> ReadToken(string token)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = _audienceId,
                IssuerSigningToken = new BinarySecretSecurityToken(_keyByteArray),
                ValidIssuer = _issuer,
            };
            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = null;

            try
            {
                ClaimsPrincipal claimP = handler.ValidateToken(token, validationParameters, out securityToken);
                var claims = claimP.Claims;
                foreach (Claim c in claims)
                {
                    dic.Add(c.Type, c.Value);
                }

                return dic;
            }
            catch (Exception e)
            {
                //add logging
                return null;
            }
        }
    }
}
