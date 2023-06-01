using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using static System.Net.WebRequestMethods;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Tag")]
    [ApiController]
    [Authorize]
    public class SCRMTagController : ControllerBase
    {
        private readonly SCRMITagRepository _tagRepository;
        private readonly IAWSImageService _imageService;

        public SCRMTagController(SCRMITagRepository tagRepository, IAWSImageService imageService)
        {
            _tagRepository = tagRepository;
            _imageService = imageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTag([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.GetAllTagAsync(requestParams);
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.TagImageUrl = !string.IsNullOrEmpty(record.TagImageUrl) ? _imageService.GetImageBaseUrl() + record.TagImageUrl : string.Empty;
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

        [HttpGet("Active")]
        public async Task<IActionResult> GetAllActiveTag()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.GetAllActiveTagAsync();
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.TagImageUrl = !string.IsNullOrEmpty(record.TagImageUrl) ? _imageService.GetImageBaseUrl() + record.TagImageUrl : string.Empty;
                    }

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
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.GetTagByIdAsync(id);
                if (data != null)
                {
                    data.TagImageUrl = !string.IsNullOrEmpty(data.TagImageUrl) ? _imageService.GetImageBaseUrl() + data.TagImageUrl : string.Empty;

                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Tag for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTag([FromForm] SCRMTagModel model)
        {

            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.AddTagAsync(model);
                if (data != null)
                {
                    data.TagImageUrl = !string.IsNullOrEmpty(data.TagImageUrl) ? _imageService.GetImageBaseUrl() + data.TagImageUrl : string.Empty;

                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddTag), response);
                }
                response.error = "Please Enter Required Data for Addition of Tag";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromForm] SCRMTagModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.UpdateTagAsync(id, model);
                if (data != null)
                {
                    data.TagImageUrl = !string.IsNullOrEmpty(data.TagImageUrl) ? _imageService.GetImageBaseUrl() + data.TagImageUrl : string.Empty;

                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Tag for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateTagStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _tagRepository.UpdateTagStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Requested Tag Status for Id = {id} is Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Tag for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.GetTagByIdAsync(id);
                if (data != null)
                {
                    await _tagRepository.DeleteTagAsync(id);
                    response.success = true;
                    response.data = $"Requested Tag for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Tag for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("TagWiseTemplateList")]
        public async Task<IActionResult> TagWiseTemplateList()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _tagRepository.GetAllTagWiseTemplateListAsync();
                if (data != null)
                {
                    foreach (var tag in data)
                    {
                        foreach (var template in tag.TemplateAndLayout)
                        {
                            template.TemplateImageURL = _imageService.GetImageBaseUrl() + template.TemplateImageURL;
                            foreach (var item in template.ImageFields)
                            {
                                item.Value = _imageService.GetImageBaseUrl() + item.Value;
                            }
                        }
                    }
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Something went wrong. Please try again.";
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
