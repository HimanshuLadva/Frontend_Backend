using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        AuthResponse response = new AuthResponse();

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.SignUpAsync(model);
                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = "Signup Successfully";
                    response.Data = result;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                else if (result.Errors.FirstOrDefault()!.Code == "DuplicateUserName")
                {
                    response.error.errorMessage = $"Requested Email {model.Email} is already taken.";
                    response.Success = false;
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, response);
                }
                errorMessage = result.Errors.FirstOrDefault()!.Description;
            }
            response.error.errorMessage = errorMessage;
            response.Success = false;
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var result = await _accountRepository.LoginAsync(model);
            response.Data = result;
            if (result == null)
            {
                response.Success = false;
                response.error.errorMessage = "Invalid Email Or Password";
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            response.Success = true;
            response.Message = "User Login Successfully";
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("Me")]
        public async Task<IActionResult> MyAccount()
        {
            var claimsData = this.HttpContext!.User!.Claims.FirstOrDefault();
            if (claimsData == null)
            {
                response.Success = false;
                response.error.errorMessage = "Please Enter Valid Token";
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            response.Success = true;
            response.Message = "Token Getting Successfully";
            response.Data = await _accountRepository.MyAccountAsync(claimsData);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("FacebookLogin")]
        public async Task<IActionResult> FacebookLogin([FromBody] TokenModel accessToken)
        {
            var fbval = await _accountRepository.FacebookLogin(accessToken);
            if (fbval.Success)
            {
                UserLoginModel userModel = new UserLoginModel();
                userModel = JsonConvert.DeserializeObject<UserLoginModel>(JsonConvert.SerializeObject(fbval!.Data));
                if (userModel != null && !string.IsNullOrEmpty(userModel.Email))
                {
                    var result = _accountRepository.GetUserDetailsByEmailId(userModel.Email).Result;
                    if (result == null)
                    {
                        response.Success = false;
                        response.error.errorMessage = "Invalid Email Or Password";
                        return StatusCode(StatusCodes.Status401Unauthorized, response);
                    }
                    result.Token = userModel.AccessToken;
                    response.Data = result;
                    response.Success = true;
                    response.Message = "User Login Successfully";
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                else
                {
                    return new BadRequestObjectResult(fbval);
                }
            }
            else
                return new BadRequestObjectResult(fbval);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.LogOut();
            response.Success = true;
            response.Message = "Logout Successfully";
            return StatusCode(StatusCodes.Status200OK, response);
        }

    }
}
