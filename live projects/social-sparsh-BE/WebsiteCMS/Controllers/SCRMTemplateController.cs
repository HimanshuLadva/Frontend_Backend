using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Crypt.Dsig;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Template")]
    [ApiController]
    [Authorize]
    public class SCRMTemplateController : ControllerBase
    {
        public readonly SCRMITemplateRepository _templateRepository;
        private readonly ISCRMTemplateService _TemplateService;
        private readonly IAWSImageService _imageService;

        public SCRMTemplateController(SCRMITemplateRepository templateRepository, ISCRMTemplateService templateService, IAWSImageService imageService)
        {
            _templateRepository = templateRepository;
            _TemplateService = templateService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplate([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateRepository.GetAllTemplateAsync(requestParams);
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.TemplateImageURL = !string.IsNullOrEmpty(record.TemplateImageURL) ? _imageService.GetImageBaseUrl() + record.TemplateImageURL : string.Empty;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateRepository.GetTemplateByIdAsync(id);
                if (data != null)
                {
                    data.TemplateImageURL = !string.IsNullOrEmpty(data.TemplateImageURL) ? _imageService.GetImageBaseUrl() + data.TemplateImageURL : string.Empty;
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Template for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTemplate([FromForm] SCRMTemplateModel model)
        {


            SCRMResponse response = new();
            try
            {
                var data = await _templateRepository.AddTemplateAsync(model);
                data.TemplateImageURL = !string.IsNullOrEmpty(data.TemplateImageURL) ? _imageService.GetImageBaseUrl() + data.TemplateImageURL : string.Empty;
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddTemplate), response);
                }
                response.error = "Please Enter Required Data for Addition of Template";
                return StatusCode(502, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate([FromRoute] int id, [FromForm] SCRMTemplateModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateRepository.UpdateTemplateAsync(id, model);
                data.TemplateImageURL = _imageService.GetImageBaseUrl() + data.TemplateImageURL;
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Template for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateTemplateStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _templateRepository.UpdateTemplateStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Requested Template Status for Id = {id} is Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateRepository.GetTemplateByIdAsync(id);
                if (data != null)
                {
                    await _templateRepository.DeleteTemplateAsync(id);
                    response.success = true;
                    response.data = $"Requested Template for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{templateId}/MetaAndLayout")]
        public async Task<IActionResult> TemplateMetadateAndLayoutById([FromRoute] int templateId)
        {
            SCRMResponse response = new();
            try
            {
                var model = await _TemplateService.TemplateMetadateAndLayoutByIdAsync(templateId);
                if (model != null)
                {
                    response.data = model;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Template for Id = {templateId} is Not Found...!";
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
