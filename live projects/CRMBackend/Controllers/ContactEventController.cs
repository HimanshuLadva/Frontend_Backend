using CRMBackend.Data.Interface;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ContactEventController : ControllerBase
    {
        private readonly IContactEventRepo _repo;

        public ContactEventController(IContactEventRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AssignEventToContact/{EventId}")]
        public async Task<IActionResult> AssignEventToContact([FromRoute] int EventId, [FromForm] string ContactCollection)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AssignEventToContact(EventId, ContactCollection);
                if (data)
                {
                    response.message = "Event Assign To Contact Successful";
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

        [HttpPost("AssignContactToEvent/{ContactId}")]
        public async Task<IActionResult> AssignContactToEvent([FromRoute] int ContactId, [FromForm] string EventCollection)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AssignContactToEvent(ContactId, EventCollection);
                if (data)
                {
                    response.message = "Contact Assign To Event Successful";
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
