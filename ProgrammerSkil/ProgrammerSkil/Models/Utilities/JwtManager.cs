using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ProgrammerSkil.Models.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ProgrammerSkil.Models.BLL
{
    public class JwtManager 
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>

      
 


        #region SECRET KEY
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        #endregion

        #region   JJWTManager
        public static string GenerateToken(int Id, string Role, string UserName, string SerialNumber, int expireMinutes = 10)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "backend",
                Issuer = "Issuer",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("_Id", Id.ToString()),
                    new Claim("UserRole", Role),
                    new Claim("UserName",UserName),
                    new Claim("SerialNumber", SerialNumber)
                }),
                Expires = now.AddDays(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }





        public static ClaimsPrincipal GetPrincipalFromToken(string jwtToken)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;
                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                // validationParameters.ValidateLifetime = true;

                validationParameters.ValidAudience = "backend";
                validationParameters.ValidIssuer = "Issuer";
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret));
                // validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Convert.FromBase64String(Secret); Encoding.UTF8.GetBytes(Secret));
                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion





    }
}