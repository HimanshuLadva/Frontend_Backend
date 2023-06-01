using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Caption")]
    [ApiController]
    [Authorize]
    public class SCRMCaptionController : ControllerBase
    {

        private readonly ISCRMCaptionService _capService;

        public SCRMCaptionController(ISCRMCaptionService capService)
        {
            _capService = capService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCaption()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.GetAllCaptionAsync();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaptionById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.GetCaptionById(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Caption for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("CategoryId/{id}")]
        public async Task<IActionResult> GetCaptionByCategoryId([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.GetCaptionByCategoryId(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Caption for Category Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("SubCategoryId/{id}")]
        public async Task<IActionResult> GetCaptionBySubCategoryId([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.GetCaptionBySubCategoryId(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Caption for subcategory Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCaption([FromBody] SCRMCaptionsModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.AddCaptionAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddCaption), response);
                }
                response.error = "Please Enter Required Data for Addition of Caption...!";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateCaption([FromBody] SCRMCaptionsModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.EditCaptionAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Caption for Id = {model.Id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaption([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _capService.GetCaptionById(id);
                if (data != null)
                {
                    await _capService.DeleteCaptionAsync(id);
                    response.success = true;
                    response.data = $"Requested Align for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Align for Id = {id} is Not Found...!";
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