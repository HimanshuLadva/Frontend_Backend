using CRMBackend.Data.Interface;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Mvc;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;

        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCateogryAsync([FromForm] CategoryModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddCateogryAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Category Added Successful";
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

        [HttpGet("GetAllCategory/{groupId}")]
        public async Task<IActionResult> GetAllCategoryAsync([FromRoute] int groupId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllCategoryAsync(groupId);
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

        [HttpGet("GetCategoryById/{groupId}/{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int groupId, [FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetCategoryByIdAsync(groupId, id);
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

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromForm] CategoryModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateCategoryAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Category Updated Successful";
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

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteCategoryAsync(id);
                if (data)
                {
                    response.data = data;
                    response.message = "Category Deleted Successful";
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
    }
}
