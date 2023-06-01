using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.PTG;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/BackgroundColor")]
    [ApiController]
    [Authorize]
    public class SCRMBackgroundColorController : ControllerBase
    {
        private SCRMIBackgroundColorRepository _backgroundColorRepository;
        public SCRMBackgroundColorController(SCRMIBackgroundColorRepository backgroundColorRepository)
        {
            _backgroundColorRepository = backgroundColorRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBackgroundColors([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _backgroundColorRepository.GetAllColorsAsync(requestParams);
                if (data != null)
                {
                    ResponseMetadata<object> metaData = new()
                    {
                        page_number = data.PageNumber,
                        page_size = data.PageSize,
                        records = data,
                        total_record_count = requestParams.recordCount
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackgroundColorById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _backgroundColorRepository.GetBackgroundColorByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Background Color for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBackgroundColor([FromForm] SCRMBackgroundColorModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _backgroundColorRepository.AddBackgroundColorAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddBackgroundColor), response);
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
