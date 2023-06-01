using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class UserNoteController : ControllerBase
    {
        private readonly IUserNoteRepo _repo;

        public UserNoteController(IUserNoteRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddNote/{contactId}")]
        public async Task<IActionResult> AddNoteAsync([FromRoute] int contactId, [FromForm] UserNoteModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddNoteAsync(contactId, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Note Added Successful";
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

        [HttpGet("GetAllNote/{contactId}")]
        public async Task<IActionResult> GetAllNoteAsync([FromRoute] int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllNoteAsync(contactId);
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

        [HttpGet("GetNoteById/{contactId}/{id}")]
        public async Task<IActionResult> GetNoteByIdAsync([FromRoute] int id, [FromRoute] int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetNoteByIdAsync(id, contactId);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Note for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateNote/{contactId}/{id}")]
        public async Task<IActionResult> UpdateNoteAsync([FromRoute] int contactId, [FromRoute] int id, [FromForm] UserNoteModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateNoteAsync(contactId, id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Note Updated Successful";
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

        [HttpDelete("DeleteNote/{contactId}/{id}")]
        public async Task<IActionResult> DeleteNoteAsync([FromRoute] int contactId, [FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteNoteAsync(contactId, id);
                if (data)
                {
                    response.data = data;
                    response.message = "Note Deleted Successful";
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
