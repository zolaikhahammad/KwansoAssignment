using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core
{
    public class TokenServices
    {
        public static async Task<JwtToken> GenerateTokenAsync(string email, string user_id)
        {
            JwtToken _jwttoken = new JwtToken();
            try
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("klmnhi0987yhgaqwetyhncvasdvgh098");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                           
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, email),
                            new Claim("id", user_id),
                        
                        }),
                    Expires = DateTime.UtcNow.AddHours(87600),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenhandler.CreateToken(tokenDescriptor);
                _jwttoken.token = tokenhandler.WriteToken(token);
            }
            catch (Exception ex)
            {

            }
            return _jwttoken;
        }

    }
}
