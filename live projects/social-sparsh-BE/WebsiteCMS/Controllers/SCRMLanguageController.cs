using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Language")]
    [ApiController]
    [Authorize]
    public class SCRMLanguageController : ControllerBase
    {
        private readonly SCRMILanguageRepository _languageRepository;
        public SCRMLanguageController(SCRMILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [HttpGet("GetAllLanguages")]
        public async Task<IActionResult> GetAllLanguages([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _languageRepository.GetAllLanguageAsync(requestParams);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLanguageById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _languageRepository.GetLanguageByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Data not found!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddLanguage([FromForm] SCRMLanguageModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _languageRepository.AddLanguageAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddLanguage), response);
                }
                response.error = "Please Enter Required Data";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage([FromRoute] int id, [FromForm] SCRMLanguageModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _languageRepository.UpdateLanguageAsync(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Something went wrong. Please try again";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateLanguageStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _languageRepository.UpdateLanguageStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Language status updated successfully...!";
                    return Ok(response);
                }
                response.error = $"Something went wrong. Please try again";
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
