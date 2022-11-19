using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Setsis_Fullstack_Case.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }







    }
}
