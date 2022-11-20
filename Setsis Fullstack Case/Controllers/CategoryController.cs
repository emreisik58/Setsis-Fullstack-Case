using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models.Entity;
using System.Text;
using Tridi_Case_Study.Services;

namespace Setsis_Fullstack_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private IConfiguration _config;
        private ICategoryProviderRepo _CategoryProviderRepo;
        private ITokenRepo _tokenRepo;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryProviderRepo UserProviderRepo, IConfiguration config, ITokenRepo tokenRepo, ILogger<CategoryController> logger)
        {
            _config = config;
            _logger = logger;
            _CategoryProviderRepo = UserProviderRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpGet]
        [Route("GetCategoryById")]
        [Authorize]
        public BusinessLayerResult GetCategoryById(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            Category category = _CategoryProviderRepo.GetById(id);
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(category);
            return response;
        }
        [HttpGet]
        [Route("GetCategoryList")]
        [Authorize]
        public BusinessLayerResult GetCategoryList()
        {
            BusinessLayerResult response = new BusinessLayerResult();
            List<Category> categorys = _CategoryProviderRepo.GetByListCategory();
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(categorys);
            return response;
        }

        [HttpPost]
        [Route("AddCategory")]
        [Authorize]
        public async Task<BusinessLayerResult> AddCategory()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            Category category = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(bodyData);
            var id = _CategoryProviderRepo.Add(category);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }

        [HttpPost]
        [Route("CategoryUpdate")]
        [Authorize]
        public async Task<BusinessLayerResult> CategoryUpdate()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            Category category = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(bodyData);
            var id = _CategoryProviderRepo.Update(category);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }
        [HttpGet]
        [Route("CategoryDelete")]
        [Authorize]
        public BusinessLayerResult CategoryDelete(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            _CategoryProviderRepo.GetByIdRemove(id);
            response.isSuccess = true;
            response.data = id.ToString() + " numaralı user silindi.. ";
            return response;
        }

    }
}
