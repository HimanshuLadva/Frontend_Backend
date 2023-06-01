using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/TemplateLayout")]
    [ApiController]
    [Authorize]
    public class SCRMTemplateLayoutController : ControllerBase
    {
        public readonly SCRMITemplateLayoutRepository _templateLayoutRepository;

        public SCRMTemplateLayoutController(SCRMITemplateLayoutRepository templateLayoutRepository)
        {
            _templateLayoutRepository = templateLayoutRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTemplateLayout([FromQuery] SCRMRequestParams requestParams)
        {
            var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
            SCRMResponse response = new();
            try
            {
                var data = await _templateLayoutRepository.GetAllTemplateLayoutAsync(requestParams);
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.TemplateImageURL = !string.IsNullOrEmpty(record.TemplateImageURL) ? baseURL + "/" + record.TemplateImageURL : string.Empty;
                        foreach (var field in record.ImageFields)
                        {
                            field.Value = !string.IsNullOrEmpty(field.Value) ? baseURL + field.Value : string.Empty;
                        }
                    }

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

        [HttpGet("{templateId}")]
        public async Task<IActionResult> GetTemplateLayoutById([FromRoute] int templateId)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(templateId);
                data.TemplateImageURL = !string.IsNullOrEmpty(data.TemplateImageURL) ? data.TemplateImageURL : string.Empty;
                foreach (var record in data.ImageFields)
                {
                    record.Value = !string.IsNullOrEmpty(record.Value) ? record.Value : string.Empty;
                }

                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Template Layout for Id = {templateId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{templateId}")]
        public async Task<IActionResult> UpdateTemplateLayout([FromRoute] int templateId, [FromBody] SCRMTemplateLayoutModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateLayoutRepository.UpdateTemplateLayoutAsync(templateId, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Template Layout for Id = {templateId} is Not Found...!";
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
