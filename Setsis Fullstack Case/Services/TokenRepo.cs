using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Setsis_Fullstack_Case.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Setsis_Fullstack_Case.Services
{
    public class TokenRepo: ITokenRepo
    {
       public string GenerateJSONWebToken(string userName,string key,string issuer,string Audience)
       {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userName)
            };
            var token = new JwtSecurityToken(issuer, Audience, claims, expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
       }
        public string GetCurrentUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                try
                {
                    var userClaims = identity.Claims;
                    return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return null;
        }




    }
}
