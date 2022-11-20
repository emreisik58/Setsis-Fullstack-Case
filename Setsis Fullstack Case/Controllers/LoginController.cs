using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models.Entity;
using Setsis_Fullstack_Case.Services;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tridi_Case_Study.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Setsis_Fullstack_Case.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserProviderRepo _UserProviderRepo;
        private ITokenRepo _tokenRepo;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserProviderRepo UserProviderRepo, IConfiguration config, ITokenRepo tokenRepo, ILogger<LoginController> logger)
        {
            _config = config;
            _logger = logger;
            _UserProviderRepo = UserProviderRepo;
            _tokenRepo = tokenRepo;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<BusinessLayerResult> Post()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();

            BusinessLayerResult response = new BusinessLayerResult();
            try
            {
                RequestLogin login = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestLogin>(bodyData);
                bool isUserRequired = _UserProviderRepo.UserRequired(login.user,login.password);
                if (isUserRequired)
                {

                    string tokenString = _tokenRepo.GenerateJSONWebToken(login.user,_config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
                    response.data = tokenString;
                    response.isSuccess = true;

                }
                else 
                {

                    response.isSuccess = false;
                    response.errors = "Kullanıcı Adı yada Şifre hatalı";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.errors = ex.Message;
            }
            return response;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<BusinessLayerResult> Register()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(bodyData);
             var id = _UserProviderRepo.Add(user);
                response.data = id.ToString();
                response.isSuccess = true;
            return response;
        }





        [HttpGet]
        [Route("TokenToUser")]
        [Authorize]
        public BusinessLayerResult TokenToUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            BusinessLayerResult response = new BusinessLayerResult();           
            response.data = _tokenRepo.GetCurrentUser(identity);
            response.isSuccess = true;
            return response;
        }
        private class RequestLogin
        {
            public string user { get; set; }
            public string password { get; set; }

        }

    }
}

