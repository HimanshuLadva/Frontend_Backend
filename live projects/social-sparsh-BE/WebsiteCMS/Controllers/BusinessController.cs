using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        [HttpPost("AddBusiness")]
        public async Task<IActionResult> AddBusiness([FromForm] BusinessModel model)
        {

            SCRMResponse response = new();
            try
            {
                var data = await _businessService.AddBusiness(model);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return Created(nameof(model), response);
                }
                response.error = "Please Enter Required Data for Addition of Business Detail";
                return StatusCode(502, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBusinessAccountDetail([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _businessService.GetAllBusinessDetail(requestParams);
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
                response.error = "There is No Business Details";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllOneBusinessAccountDetail([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _businessService.GetBusinessDetailByIdAsync(id);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Business Detail for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessDetail([FromRoute] int id, [FromForm] BusinessModel model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _businessService.UpdateBusinessDetail(id, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested Business Detail for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessDetail([FromRoute] int id)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _businessService.DeleteBusinessDetailAsync(id);
                if (data)
                {
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Requested Business Detail for Id = {id} is Not Found...!";
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
