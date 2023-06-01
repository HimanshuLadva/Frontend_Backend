using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/FontFamily")]
    [ApiController]
    [Authorize]
    public class SCRMFontFamilyController : ControllerBase
    {
        private readonly SCRMIFontFamilyRepository _fontFamilyRepository;

        public SCRMFontFamilyController(SCRMIFontFamilyRepository fontFamilyRepository)
        {
            _fontFamilyRepository = fontFamilyRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllFontFamily()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _fontFamilyRepository.GetAllFontFamilyAsync();
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
        public async Task<IActionResult> GetFontFamilyById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _fontFamilyRepository.GetFontFamilyByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Font Family for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddFontFamily([FromBody] SCRMFontFamilyModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _fontFamilyRepository.AddFontFamilyAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddFontFamily), response);
                }
                response.error = "Please Enter Required Data for Addition of Font Family";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFontFamily([FromRoute] int id, [FromBody] SCRMFontFamilyModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _fontFamilyRepository.UpdateFontFamilyAsync(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Font Family for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateFontFamilyStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _fontFamilyRepository.UpdateFontFamilyStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = "Font Family Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Font Family for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFontFamily([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _fontFamilyRepository.GetFontFamilyByIdAsync(id);
                if (data != null)
                {
                    await _fontFamilyRepository.DeleteFontFamilyAsync(id);
                    response.success = true;
                    response.data = $"Requested Font Family for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Font Family for Id = {id} is Not Found...!";
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
