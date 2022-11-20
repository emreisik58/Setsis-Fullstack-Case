using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Setsis_Fullstack_Case.Infrastructure
{
    public interface ITokenRepo
    {
        public string GenerateJSONWebToken(string userName, string key, string issuer, string Audience);
        public string GetCurrentUser(ClaimsIdentity identity);
    }
}
