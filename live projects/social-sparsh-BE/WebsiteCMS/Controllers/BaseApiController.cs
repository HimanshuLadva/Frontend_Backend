using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebsiteCMS.DAL.Data.Interface;

namespace WebsiteCMS.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        [NonAction]
        public string GetUserId()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
