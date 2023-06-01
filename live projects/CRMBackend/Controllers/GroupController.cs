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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepo _repo;

        public GroupController(IGroupRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroupAsync([FromForm] GroupModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddGroupAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    response.message = "Group Added Successful";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllGroup")]
        public async Task<IActionResult> GetAllGroupAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllGroupAsync();
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

        [HttpGet("GetGroupById/{id}")]
        public async Task<IActionResult> GetGroupByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetGroupByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Group for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<IActionResult> UpdateGroupAsync([FromRoute] int id, [FromForm] GroupModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateGroupAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    response.message = "Group Updated Successful";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroupAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteGroupAsync(id);
                if (data)
                {
                    response.data = data;
                    response.success = true;
                    response.message = "Group Deleted Successful";
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
