using CRMBackend.Data.Interface;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepo _repo;

        public ContactController(IContactRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContactAsync([FromForm] ContactModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddContactAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Contact Added Successful";
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

        [HttpGet("GetAllContact")]
        public async Task<IActionResult> GetAllContactAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllContactAsync();
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("GetContactById/{id}")]
        public async Task<IActionResult> GetContactByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetContactByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Contact for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] int id, [FromForm] ContactModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateContactAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Contact Updated Successful";
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

        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteContactAsync(id);
                if (data)
                {
                    response.data = data;
                    response.success = true;
                    response.message = "Contact Deleted Successful";
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
