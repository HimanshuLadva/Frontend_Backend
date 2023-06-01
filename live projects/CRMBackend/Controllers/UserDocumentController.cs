using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
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
    public class UserDocumentController : ControllerBase
    {
        private readonly IUserDocumentRepo _repo;

        public UserDocumentController(IUserDocumentRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddDocument/{contactId}")]
        public async Task<IActionResult> AddDocumentAsync([FromRoute] int contactId, [FromForm] UserDocumentModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.AddDocumentAsync(contactId, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Document Added Successful";
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

        [HttpGet("GetAllDocument/{contactId}")]
        public async Task<IActionResult> GetAllDocumentAsync(int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetAllDocumentAsync(contactId);
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

        [HttpGet("GetDocumentById/{contactId}/{id}")]
        public async Task<IActionResult> GetDocumentByIdAsync([FromRoute] int id, [FromRoute] int contactId)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.GetDocumentByIdAsync(id, contactId);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Document for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("UpdateDocument/{contactId}/{id}")]
        public async Task<IActionResult> UpdateDocumentAsync([FromRoute] int contactId, [FromRoute] int id, [FromForm] UserDocumentModel model)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.UpdateDocumentAsync(contactId, id, model);
                if (data != null)
                {
                    response.data = data;
                    response.message = "Document Updated Successful";
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

        [HttpDelete("DeleteDocument/{contactId}/{id}")]
        public async Task<IActionResult> DeleteDocumentAsync([FromRoute] int contactId, [FromRoute] int id)
        {
            RMResponse response = new();
            try
            {
                var data = await _repo.DeleteDocumentAsync(contactId, id);
                if (data)
                {
                    response.data = data;
                    response.message = "Document Deleted Successful";
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
