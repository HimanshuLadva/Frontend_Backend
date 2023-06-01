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
    public class ReminderController : ControllerBase
    {
        private readonly IReminderRepo _repo;

        public ReminderController(IReminderRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddReminder")]
        public async Task<IActionResult> AddReminderAsync([FromForm] ReminderModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddReminderAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Reminder Added Successful";
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

        [HttpGet("GetAllReminder")]
        public async Task<IActionResult> GetAllReminderAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllReminderAsync();
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

        [HttpGet("GetReminderById/{id}")]
        public async Task<IActionResult> GetReminderByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetReminderByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Reminder for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateReminder/{id}")]
        public async Task<IActionResult> UpdateReminderAsync([FromRoute] int id, [FromForm] ReminderModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateReminderAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Reminder Updated Successful";
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

        [HttpDelete("DeleteReminder/{id}")]
        public async Task<IActionResult> DeleteReminderAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteReminderAsync(id);
                if (data)
                {
                    response.data = data;
                    response.message = "Reminder Deleted Successful";
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
