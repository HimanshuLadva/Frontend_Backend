using AngularAndNetCoreAuth.Data;
using AngularAndNetCoreAuth.Models;
using AngularAndNetCoreAuth.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngularAndNetCoreAuth.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("EnableCors")]
    public class AccountController : Controller
    {
        private readonly UsersDbContext _db;
        private readonly IAuthService AuthService;


        public AccountController(UsersDbContext db, IAuthService authService)
        {
            _db = db;
            AuthService = authService;
        }

        /// <summary>
        /// This method just takes the data from our client side, that our angular-social-login provides for us, then it takes some part of that data and creates a token 
        /// that is sent back to the user. On every request, you should add to token to the request headers so the user can be authourized on every request.
        /// </summary>
        /// <param name="userdata"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FacebookLogin")]
        public async Task<IActionResult> FacebookLogin([FromBody] TokenModel accessToken)
        {
            var fbval = await AuthService.FacebookLogin(accessToken);
            if (fbval.Success)
            {
                return Ok(fbval);
            }
            else
            {
                return new BadRequestObjectResult(fbval);
            }
        }

        public IActionResult Index()
        {
            return Ok();
        }

    }
}

