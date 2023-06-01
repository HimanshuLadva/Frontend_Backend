using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Models;
using CRMBackend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepo _repo;

        public SubCategoryController(ISubCategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddSubCategory")]
        public async Task<IActionResult> AddSubCateogryAsync([FromForm] SubCategoryModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddSubCateogryAsync(model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "SubCategory Added Successful";
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

        [HttpGet("GetAllSubCategory")]
        public async Task<IActionResult> GetAllSubCategoryAsync()
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllSubCategoryAsync();
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

        [HttpGet("GetSubCategoryById/{id}")]
        public async Task<IActionResult> GetSubCategoryByIdAsync([FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetSubCategoryByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Sub Category for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategoryAsync([FromRoute] int id, [FromForm] SubCategoryModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateSubCategoryAsync(id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "SubCategory Updated Successful";
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

        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubCategoryAsync(int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteSubCategoryAsync(id);
                if (data)
                {
                    response.data = data;
                    response.message = "SubCategory Deleted Successful";
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
