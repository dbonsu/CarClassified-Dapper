using CarClassified.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Thinktecture.IdentityModel.Tokens;

namespace CarClassified.Common
{
    public class TokenUtility : ITokenUtility
    {
        private string audienceId = BaseSettings.AudienceId; //ConfigurationManager.AppSettings["audienceID"];
        private string issuer = "self";
        private byte[] keyByteArray;
        private HmacSigningCredentials signingKey;
        private string symmetricKeyAsBase64 = BaseSettings.SymmetricKey; //ConfigurationManager.AppSettings["SymmetricKey"];
        private string scheme = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public TokenUtility()
        {
            keyByteArray = Encoding.Default.GetBytes(symmetricKeyAsBase64);

            signingKey = new HmacSigningCredentials(keyByteArray);
        }

        public string GenerateToken(string email)
        {
            var issued = DateTime.UtcNow;

            var expires = DateTime.UtcNow.AddHours(24);
            var claims = new[]
            {
                new Claim("email",email),
            };

            var token = new JwtSecurityToken(issuer, audienceId, claims, issued, expires, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public string GetEmail(string token)
        {
            IDictionary<string, string> result = ReadToken(token);
            if (result != null)
            {
                string email;
                result.TryGetValue(scheme, out email);
                return email;
            }
            return "";
        }

        public IDictionary<string, string> ReadToken(string token)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = audienceId,
                IssuerSigningToken = new BinarySecretSecurityToken(keyByteArray),
                ValidIssuer = issuer,
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
                return null;
            }
        }
    }
}
