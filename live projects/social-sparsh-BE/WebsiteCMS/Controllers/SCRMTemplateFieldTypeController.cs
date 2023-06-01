using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/TemplateFieldType")]
    [ApiController]
    [Authorize]
    public class SCRMTemplateFieldTypeController : ControllerBase
    {
        private readonly SCRMITemplateFieldTypeRepository _templateFieldTypeRepository;

        public SCRMTemplateFieldTypeController(SCRMITemplateFieldTypeRepository templateFieldTypeRepository)
        {
            _templateFieldTypeRepository = templateFieldTypeRepository;

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTemplateFieldType()
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldTypeRepository.GetAllTemplateFieldTypeAsync();
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
        public async Task<IActionResult> GetTemplateFieldTypeById([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldTypeRepository.GetTemplateFieldTypeByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Template Field Type for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTemplateFieldType([FromBody] SCRMTemplateFieldTypeModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldTypeRepository.AddTemplateFieldTypeAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Created(nameof(AddTemplateFieldType), response);
                }
                response.error = "Please Enter Required Data for Addition of Template Field Type";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateFieldType([FromRoute] int id, [FromBody] SCRMTemplateFieldTypeModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldTypeRepository.UpdateTemplateFieldTypeAsync(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Template Field Type for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}/UpdateStatus")]
        public async Task<IActionResult> UpdateTemplateFieldTypeStatus([FromRoute] int id, [FromBody] SCRMUpdateStatusModel model)
        {
            SCRMResponse response = new();
            try
            {
                bool data = await _templateFieldTypeRepository.UpdateTemplateFieldTypeStatusAsync(id, model);
                if (data == true)
                {
                    response.success = true;
                    response.data = $"Requested Template Field Type Status for Id = {id} is Updated Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template Field Type for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateFieldType([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _templateFieldTypeRepository.GetTemplateFieldTypeByIdAsync(id);
                if (data != null)
                {
                    await _templateFieldTypeRepository.DeleteTemplateFieldTypeAsync(id);
                    response.success = true;
                    response.data = $"Requested Template Field Type for Id = {data.Id} is Deleted Successfully...!";
                    return Ok(response);
                }
                response.error = $"Requested Template Field Type for Id = {id} is Not Found...!";
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
