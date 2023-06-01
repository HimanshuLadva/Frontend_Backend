using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repository;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRMBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class UserEmailController : ControllerBase
    {
        private readonly IUserEmailRepo _repo;

        public UserEmailController(IUserEmailRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddEmail")]
        public async Task<IActionResult> AddEmailAsync([FromForm] UserEmailModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddEmailAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllEmail")]
        public async Task<IActionResult> GetAllEmailAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllEmailAsync();
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetEmailById/{id}")]
        public async Task<IActionResult> GetEmailByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetEmailByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Email for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteEmail/{id}")]
        public async Task<IActionResult> DeleteEmailAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteEmailAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
