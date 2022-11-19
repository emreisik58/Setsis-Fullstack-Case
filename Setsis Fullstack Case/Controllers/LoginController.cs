using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Setsis_Fullstack_Case.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Setsis_Fullstack_Case.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserProviderRepo _UserProviderRepo;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserProviderRepo UserProviderRepo, IConfiguration config, ILogger<LoginController> logger)
        {
            _config = config;
            _logger = logger;
            _UserProviderRepo = UserProviderRepo;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<JObject> Post()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();

            JObject response = new JObject();
            try
            {
                RequestLogin login = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestLogin>(bodyData);

                var user = AuthenticateUser(login);

                if (user != null)
                {
                    string tokenString = GenerateJSONWebToken(user);
                    response.Add("Token",(JValue)tokenString);
                }


            }
            catch (Exception ex)
            {

                response.Add("ErrorMsg",ex.Message);
            }
            return response;

        }

        private string GenerateJSONWebToken(RequestLogin userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RequestLogin AuthenticateUser(RequestLogin login)
        {
            RequestLogin user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.user == "Jignesh Trivedi")
            {
                user = new RequestLogin { user = "Jignesh Trivedi", password = "test.btest@gmail.com" };
            }
            return user;
        }
        private class RequestLogin
        {
            public string user { get; set; }
            public string password { get; set; }

        }

    }
}

