using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        [Route("{id:int:min(10):max(111)}")]
        public string GetById(int id)
        {
            return "Hello int " + id;
        }

        [Route("{id:length(5):alpha}")]
        public string GetByIdString(string id)
        {
            return "Hello string " + id;
        }
        
        [Route("{id:regex(a(b|c))}")]
        public string GetByIdRegex(string id)
        {
            return "Hello regex " + id;
        }
    }
}
