using CRMBackend.Data.Interface;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        AuthResponse response = new();

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpPost("SignUpForUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUpForUser([FromBody] SignUpModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var result = await _accountRepo.SignUpAsyncForUser(model);
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

        [HttpPost("SignUpForAdmin")]
        [Authorize(Roles = "MasterAdmin")]
        public async Task<IActionResult> SignUpForAdmin([FromBody] SignUpModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var result = await _accountRepo.SignUpAsyncForAdmin(model);
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
            var result = await _accountRepo.LoginAsync(model);
            response.Data = result;
            if (result == null)
            {
                response.Success = false;
                response.error.errorMessage = "Invalid Email Or Password";
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            response.Data = result;
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

            response.Data = await _accountRepo.MyAccountAsync(claimsData);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepo.LogOut();
            response.Success = true;
            response.Message = "Logout Successfully";
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
