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
    public class ProductController : ControllerBase
    {

        private IConfiguration _config;
        private IProductProviderRepo _ProductProviderRepo;
        private ITokenRepo _tokenRepo;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductProviderRepo ProductProviderRepo, IConfiguration config, ITokenRepo tokenRepo, ILogger<ProductController> logger)
        {
            _config = config;
            _logger = logger;
            _ProductProviderRepo = ProductProviderRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpGet]
        [Route("GetUserById")]
        [Authorize]
        public BusinessLayerResult GetByProductId(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            Product product  = _ProductProviderRepo.GetById(id);
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(product);
            return response;
        }
        [HttpGet]
        [Route("GetProductList")]
        [Authorize]
        public BusinessLayerResult GetProductList()
        {
            BusinessLayerResult response = new BusinessLayerResult();
            List<Product> products = _ProductProviderRepo.GetByListProduct();
            response.isSuccess = true;
            response.data = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            return response;
        }

        [HttpPost]
        [Route("AddProduct")]
        [Authorize]
        public async Task<BusinessLayerResult> AddProduct()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            Product product  = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(bodyData);
            var id = _ProductProviderRepo.Add(product);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }

        [HttpPost]
        [Route("UpdateProduct")]
        [Authorize]
        public async Task<BusinessLayerResult> ProductUpdate()
        {
            string bodyData = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            BusinessLayerResult response = new BusinessLayerResult();
            Product product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(bodyData);
            var id = _ProductProviderRepo.Update(product);
            response.data = id.ToString();
            response.isSuccess = true;
            return response;
        }
        [HttpGet]
        [Route("DeleteProduct")]
        [Authorize]
        public BusinessLayerResult ProductDelete(int id)
        {
            BusinessLayerResult response = new BusinessLayerResult();
            _ProductProviderRepo.GetByIdRemove(id);
            response.isSuccess = true;
            response.data = id.ToString() + " numaralı user silindi.. ";
            return response;
        }




    }
}
