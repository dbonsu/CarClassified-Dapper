﻿using CarClassified.Common.Interfaces;
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
        private string audienceId = ConfigurationManager.AppSettings["audienceID"];
        private string issuer = "self";
        private byte[] keyByteArray;
        private HmacSigningCredentials signingKey;
        private string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["SymmetricKey"];

        public TokenUtility()
        {
            keyByteArray = Encoding.Default.GetBytes(symmetricKeyAsBase64);

            signingKey = new HmacSigningCredentials(keyByteArray);
        }

        public string GenerateToken(string email)
        {
            var issued = DateTime.UtcNow;

            var expires = DateTime.UtcNow.AddHours(5);
            var claims = new[]
            {
                new Claim("email",email),
            };

            var token = new System.IdentityModel.Tokens.JwtSecurityToken(issuer, audienceId, claims, issued, expires, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public string GetEmail(string token)
        {
            IDictionary<string, string> result = ReadToken(token);
            string email;
            result.TryGetValue("email", out email);
            return email;
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
            ClaimsPrincipal claimP = handler.ValidateToken(token, validationParameters, out securityToken);
            var claims = claimP.Claims;
            foreach (Claim c in claims)
            {
                dic.Add(c.Type, c.Value);
            }

            return dic;
        }
    }
}