using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
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
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoRepo _repo;

        public UserPhotoController(IUserPhotoRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddPhoto/{contactId}")]
        public async Task<IActionResult> AddPhotoAsync([FromRoute] int contactId, [FromForm] UserPhotoModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddPhotoAsync(contactId, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Photo Added Successful";
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

        [HttpGet("GetAllPhoto/{contactId}")]
        public async Task<IActionResult> GetAllPhotoAsync([FromRoute] int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllPhotoAsync(contactId);
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

        [HttpGet("GetPhotoById/{contactId}/{id}")]
        public async Task<IActionResult> GetPhotoByIdAsync([FromRoute] int id, [FromRoute] int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetPhotoByIdAsync(id, contactId);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Photo for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdatePhoto/{contactId}/{id}")]
        public async Task<IActionResult> UpdatePhotoAsync([FromRoute] int contactId, [FromRoute] int id, [FromForm] UserPhotoModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdatePhotoAsync(contactId, id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Photo Updated Successful";
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

        [HttpDelete("DeletePhoto/{contactId}/{id}")]
        public async Task<IActionResult> DeletePhotoAsync([FromRoute] int contactId, [FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeletePhotoAsync(contactId, id);
                if (data)
                {
                    response.data = data;
                    response.message = "Photo Deleted Successful";
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
