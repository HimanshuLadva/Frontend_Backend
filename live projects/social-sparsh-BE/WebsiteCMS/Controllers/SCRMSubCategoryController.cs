using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/SubCategory")]
    [ApiController]
    [Authorize]
    public class SCRMSubCategoryController : ControllerBase
    {
        private readonly SCRMISubCategoryRepository _subCategoryRepository;
        private readonly ISCRMSubCategoryServcie _subCategoryServcie;
        private readonly IAWSImageService _imageService;

        public SCRMSubCategoryController(SCRMISubCategoryRepository subCategoryRepository, ISCRMSubCategoryServcie subCategoryServcie, IAWSImageService imageService)
        {
            _subCategoryRepository = subCategoryRepository;
            _subCategoryServcie = subCategoryServcie;
            _imageService = imageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllSubCategory([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetAllSubCategoryAsync(requestParams);
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

        [HttpGet("Active")]
        public async Task<IActionResult> GetAllActiveSubCategory()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetAllActiveSubCategoryAsync();
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
        public async Task<IActionResult> GetSubCategoryById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddSubCategory([FromForm] SCRMSubCategoryModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.AddSubCategoryAsync(model);
                data.SubCategoryImageURL = !string.IsNullOrEmpty(data.SubCategoryImageURL) ? data.SubCategoryImageURL : string.Empty;

                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddSubCategory), response);
                }
                response.error = "Please Enter Required Data for Addition of Category";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] int id, [FromForm] SCRMSubCategoryModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.UpdateSubCategoryAsync(id, model);
                if (data != null)
                {
                    data.SubCategoryImageURL = !string.IsNullOrEmpty(data.SubCategoryImageURL) ? _imageService.GetImageBaseUrl() + data.SubCategoryImageURL : string.Empty;
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateSubCategoryStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _subCategoryRepository.UpdateSubCategoryStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Requested Category Status for Id = {id} is Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
                if (data != null)
                {
                    await _subCategoryRepository.DeleteSubCategoryAsync(id);
                    response.success = true;
                    response.data = $"Requested Category for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("SubCategoryWiseTemplateList/{id}")]
        public async Task<IActionResult> ParticularSubCategoryWiseTemplateList([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryServcie.GetSubCategoryWiseTemplateListAsync(id);
                if (data != null)
                {

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

        [HttpGet("SubCategoryWiseTemplateList")]
        public async Task<IActionResult> SubCategoryWiseTemplateList()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetAllSubCategoryWiseTemplateListAsync();
                if (data != null)
                {

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

        [HttpGet("CategoryWiseSubcategory/{id}")]
        public async Task<IActionResult> SubCategoryWiseSubCategory([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _subCategoryRepository.GetCategoryWiseSubCategory(id);
                if (data != null)
                {
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
