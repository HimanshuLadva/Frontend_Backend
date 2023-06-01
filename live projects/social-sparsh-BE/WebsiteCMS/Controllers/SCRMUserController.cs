using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/User")]
    [ApiController]
    [Authorize]
    public class SCRMUserController : ControllerBase
    {
        public readonly SCRMIUserRepository _userRepository;

        public SCRMUserController(SCRMIUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUser([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userRepository.GetAllUserAsync(requestParams);
                if (data != null)
                {
                    ResponseMetadata<object> metaData = new()
                    {
                        page_number = data.PageNumber,
                        page_size = data.PageSize,
                        total_record_count = requestParams.recordCount,
                        records = data
                    };
                    response.data = metaData;
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

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserById([FromHeader] string userId)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userRepository.GetUserByIdAsync(userId);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested User for Id = {userId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUser([FromBody] SCRMUserModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userRepository.AddUserAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddUser), response);
                }
                response.error = "Please Enter Required Data for Addition of User";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] SCRMUserModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userRepository.UpdateUserAsync(userId, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested User for Id = {userId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userRepository.GetUserByIdAsync(userId);
                if (data != null)
                {
                    await _userRepository.DeleteUserAsync(userId);
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested User for Id = {userId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
