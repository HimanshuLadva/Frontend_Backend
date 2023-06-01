using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebsiteCMS.Controllers
{
    [ApiController]
    [Authorize]
    public class SCRMHomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var project = "SocialCRM";
            var apiVerson = "V1";
            return Ok(new { project, apiVerson });
        }
    }
}
