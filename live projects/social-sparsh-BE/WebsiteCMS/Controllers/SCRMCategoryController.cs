using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/Category")]
    [ApiController]
    [Authorize]
    public class SCRMCategoryController : ControllerBase
    {
        private readonly SCRMICategoryRepository _categoryRepository;
        private readonly ISCRMCategoryService _categoryService;
        private readonly IAWSImageService _imageService;

        public SCRMCategoryController(SCRMICategoryRepository categoryRepository, ISCRMCategoryService categoryService, IAWSImageService imageService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCategory([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.GetAllCategoryAsync(requestParams);
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
        public async Task<IActionResult> GetAllActiveCategory()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.GetAllActiveCategoryAsync();
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
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.GetCategoryByIdAsync(id);
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
        public async Task<IActionResult> AddCategory([FromForm] SCRMCategoryModel model)
        {
            //var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.AddCategoryAsync(model);
                if (data != null)
                {
                    data.CategoryImageUrl = !string.IsNullOrEmpty(data.CategoryImageUrl) ? data.CategoryImageUrl : string.Empty;
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddCategory), response);
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
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromForm] SCRMCategoryModel model)
        {
            //var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.UpdateCategoryAsync(id, model);
                if (data != null)
                {
                    data.CategoryImageUrl = !string.IsNullOrEmpty(data.CategoryImageUrl) ? _imageService.GetImageBaseUrl() + data.CategoryImageUrl : string.Empty;
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
        public async Task<IActionResult> UpdateCategoryStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _categoryRepository.UpdateCategoryStatusAsync(id, model);
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
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.GetCategoryByIdAsync(id);
                if (data != null)
                {
                    await _categoryRepository.DeleteCategoryAsync(id);
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

        [HttpGet("CategoryWiseTemplateList")]
        public async Task<IActionResult> CategoryWiseTemplateList([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryRepository.GetAllCategoryWiseTemplateListAsync(requestParams);
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

        [HttpGet("CategoryWiseTemplateList/{id}")]
        public async Task<IActionResult> ParticularCategoryWiseTemplateList([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.GetCategoryWiseTemplateListAsync(id);
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

        [HttpGet("MultipleCategoryWiseTemplateList")]
        public async Task<IActionResult> GetAllMultipleCategoryWiseTemplateList([FromQuery] SCRMMultipleCategorys categorys)
        {

            SCRMResponse response = new();
            try
            {
                List<SCRMMultipleCategoryWiseTemplateModel> data = await _categoryRepository.GetAllMultipleCategoryWiseTemplateListAsync(categorys);
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
        //public async Task<List<SCRMMultipleCategoryWiseTemplateModel>> GetAllMultipleCategoryWiseTemplateListAsync()
    }
}
