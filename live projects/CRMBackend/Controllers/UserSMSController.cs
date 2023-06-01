using CRMBackend.Data.Interface;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class UserSMSController : ControllerBase
    {
        private readonly IUserSMSRepo _repo;

        public UserSMSController(IUserSMSRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddSMS")]
        public async Task<IActionResult> AddSMSAsync([FromForm] UserSMSModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddSMSAsync(model);
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

        [HttpGet("GetAllSMS")]
        public async Task<IActionResult> GetAllSMSAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllSMSAsync();
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

        [HttpGet("GetSMSById/{id}")]
        public async Task<IActionResult> GetSMSByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetSMSByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested SMS for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteSMS/{id}")]
        public async Task<IActionResult> DeleteSMSAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteSMSAsync(id);
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
