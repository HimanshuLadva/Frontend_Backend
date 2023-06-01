using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/TemplateField")]
    [ApiController]
    [Authorize]
    public class SCRMTemplateFieldController : ControllerBase
    {
        private readonly SCRMITemplateFieldRepository _templateFieldRepository;
        private readonly IAWSImageService _imageService;

        public SCRMTemplateFieldController(SCRMITemplateFieldRepository templateFieldRepository, IAWSImageService imageService)
        {
            _templateFieldRepository = templateFieldRepository;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplateField([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldRepository.GetAllTemplateFieldAsync(requestParams, _imageService.GetImageBaseUrl());
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.Value = record.TemplateFieldTypeId == 2 ? !string.IsNullOrEmpty(record.Value) ? _imageService.GetImageBaseUrl() + record.Value : string.Empty : record.Value;
                    }

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
        public async Task<IActionResult> GetTemplateFieldByID([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldRepository.GetTemplateFieldByIdAsync(id);
                if (data != null)
                {
                    data.Value = data.TemplateFieldTypeId == 2 ? !string.IsNullOrEmpty(data.Value) ? _imageService.GetImageBaseUrl() + data.Value : string.Empty : data.Value;
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Template Field for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTemplateField([FromForm] SCRMTemplateFieldModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldRepository.AddTemplateFieldAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Created(nameof(AddTemplateField), response);
                }
                response.error = "Please Enter Required Data for Addition of Template Field";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateField([FromRoute] int id, [FromForm] SCRMTemplateFieldModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldRepository.UpdateTemplateFieldAsync(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Template Field for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateTemplateFieldStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _templateFieldRepository.UpdateTemplateFieldStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Requested Template Field Status for Id = {id} is Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template Field for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateField([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldRepository.GetTemplateFieldByIdAsync(id);
                if (data != null)
                {
                    await _templateFieldRepository.DeleteTemplateFieldAsync(id);
                    response.success = true;
                    response.data = $"Requested Template Field for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template Field for Id = {id} is Not Found...!";
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
