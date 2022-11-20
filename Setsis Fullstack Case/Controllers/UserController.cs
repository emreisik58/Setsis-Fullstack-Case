using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models.Entity;
using System.Text;
using Tridi_Case_Study.Services;

namespace Setsis_Fullstack_Case.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private IUserProviderRepo _UserProviderRepo;
        private ITokenRepo _tokenRepo;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserProviderRepo UserProviderRepo, IConfiguration config, ITokenRepo tokenRepo, ILogger<UserController> logger)
        {
            _config = config;
            _logger = logger;
            _UserProviderRepo = UserProviderRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpGet]
        [Route("GetUserById")]
        [Authorize]
        public BusinessLayerResult GetUserById(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            User user = _UserProviderRepo.GetById(id);
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            return response;
        }
        [HttpGet]
        [Route("GetUserList")]
        [Authorize]
        public BusinessLayerResult GetUserList()
        {
            BusinessLayerResult response = new BusinessLayerResult();
           List<User> users = _UserProviderRepo.GetByListUser();
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(users);
            return response;
        }

        [HttpPost]
        [Route("AddUser")]
        [Authorize]
        public async Task<BusinessLayerResult> AddUser()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(bodyData);
            var id = _UserProviderRepo.Add(user);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }

        [HttpPost]
        [Route("UserUpdate")]
        [Authorize]
        public async Task<BusinessLayerResult> UserUpdate()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(bodyData);
            var id = _UserProviderRepo.Update(user);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }
        [HttpGet]
        [Route("UserDelete")]
        [Authorize]
        public BusinessLayerResult UserDelete(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            _UserProviderRepo.GetByIdRemove(id);
            response.isSuccess = true;
            response.data = id.ToString() + " numaralı user silindi.. ";
            return response;
        }




    }
}
