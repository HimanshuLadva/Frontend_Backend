using ConsoleToWebApi.Models;
using ConsoleToWebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRespository _productRespository, _productRespository1;

        public ProductController(IProductRespository productRespository, IProductRespository productRespository1)
        {
            _productRespository = productRespository; 
            _productRespository1 = productRespository1; 
        }

        [HttpPost("")]
        public IActionResult AddProduct([FromBody]ProductModel model)
        {
            _productRespository.AddProduct(model); 
            var products = _productRespository1.GetAllProducts();
            return Ok(products);     
        }

        [HttpGet("")]
        public IActionResult GetName()
        {
            var name = _productRespository1.GetName();
            return Ok(name);
        }

    }
}
