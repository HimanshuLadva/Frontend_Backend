using CRMBackend.Data.Interface;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ContactGroupController : ControllerBase
    {
        private readonly IContactGroupRepo _repo;

        public ContactGroupController(IContactGroupRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AssignGroupToContact/{GroupId}")]
        public async Task<IActionResult> AssignGroupToContact([FromRoute] int GroupId, [FromForm] string ContactCollection)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AssignGroupToContact(GroupId, ContactCollection);
                if (data)
                {
                    response.message = "Group Assign To Contact Successful";
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

        [HttpPost("AssignContactToGroup/{ContactId}")]
        public async Task<IActionResult> AssignContactToGroup([FromRoute] int ContactId, [FromForm] string GroupCollection)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AssignContactToGroup(ContactId, GroupCollection);
                if (data)
                {
                    response.message = "Contact Assign To Group Successful";
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
