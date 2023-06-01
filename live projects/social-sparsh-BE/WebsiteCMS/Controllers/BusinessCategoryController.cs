using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.BLL.Services;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusinessCategoryController : ControllerBase
    {
        private readonly IBusinessCategoryService _categoryService;

        public BusinessCategoryController(IBusinessCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] BusinessCategoryModel model)
        {

            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.AddCategory(model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Created(nameof(model), response);
                }
                response.error = "Please Enter Required Data for Addition of Business Category";
                return StatusCode(502, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCategoryAsync([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.GetAllCategoryAsync(requestParams);
                if (data != null)
                {
                    ResponseMetadata<object> metaData = new()
                    {
                        page_number = data.PageNumber,
                        page_size = data.PageSize,
                        total_record_count = requestParams.recordCount,
                        records = data
                    };
                    response.success = true;
                    response.data = metaData;
                    return Ok(response);
                }
                response.error = "There is No Business Categories";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.GetCategoryByIdAsync(id);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Business Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromForm] string Name)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.UpdateCategoryAsync(id, Name);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Business Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _categoryService.DeleteCategoryAsync(id);
                if (data)
                {
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Business Category for Id = {id} is Not Found...!";
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
