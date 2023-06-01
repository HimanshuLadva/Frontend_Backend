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
    public class EventController : ControllerBase
    {
        private readonly IEventRepo _repo;

        public EventController(IEventRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEventAsync([FromForm] EventModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddEventAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Event Added Successful";
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

        [HttpGet("GetAllEvent")]
        public async Task<IActionResult> GetAllEventAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllEventAsync();
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

        [HttpGet("GetEventById/{id}")]
        public async Task<IActionResult> GetEventByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetEventByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Event for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateEvent/{id}")]
        public async Task<IActionResult> UpdateEventAsync([FromRoute] int id, [FromForm] EventModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateEventAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Event Updated Successful";
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

        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEventAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteEventAsync(id);
                if (data)
                {
                    response.data = data;
                    response.message = "Event Deleted Successful";
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
