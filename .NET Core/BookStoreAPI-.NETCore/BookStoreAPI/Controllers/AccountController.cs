using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var result = await _accountRepository.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var result = await _accountRepository.LoginAsync(model);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
             return Ok(result);
        }
    }
}
