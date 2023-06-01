using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Align")]
    [ApiController]
    [Authorize]
    public class SCRMAlignController : ControllerBase
    {
        //public readonly ISCRMAlignRepository _alignRepository;

        private readonly ISCRMAlignService _alignService;

        //public SCRMAlignController(ISCRMAlignRepository alignRepository)
        //{
        //    _alignRepository = alignRepository;
        //}

        public SCRMAlignController(ISCRMAlignService alignService)
        {
            _alignService = alignService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAlign()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _alignService.GetAllAlignAsync();
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
        public async Task<IActionResult> GetAlignById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _alignService.GetAlignByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
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

        [HttpPost("")]
        public async Task<IActionResult> AddAlign([FromBody] SCRMAlignModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _alignService.AddAlignAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddAlign), response);
                }
                response.error = "Please Enter Required Data for Addition of Align...!";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlign([FromRoute] int id, [FromBody] SCRMAlignModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _alignService.UpdateAlignAsync(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlign([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _alignService.GetAlignByIdAsync(id);
                if (data != null)
                {
                    await _alignService.DeleteAlignAsync(id);
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