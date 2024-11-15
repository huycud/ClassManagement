using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassManagement.Mvc.Utilities
{
    static class ValidateToken
    {
        public static ClaimsPrincipal TokenValidation(string token, IConfiguration _configuration)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _configuration["Jwt:Issuer"],

                    ValidAudience = _configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),

                    ClockSkew = TimeSpan.Zero
                };
                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                return principal;
            }
            catch (SecurityTokenException e)
            {
                return null;
            }
        }
    }
}
