using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/UserMetaData")]
    [ApiController]
    [Authorize]
    public class SCRMUserMetaDataController : ControllerBase
    {
        private readonly SCRMIUserMetaDataRepository _userMetaDataRepository;
        private readonly IAWSImageService _imageService;

        public SCRMUserMetaDataController(SCRMIUserMetaDataRepository userMetaDataRepository, IAWSImageService imageService)
        {
            _userMetaDataRepository = userMetaDataRepository;
            _imageService = imageService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUserMetaData([FromQuery] SCRMRequestParams requestParams)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userMetaDataRepository.GetAllUserMetaDataAsync(requestParams);
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.Value = record.FieldType == "Image" ? !string.IsNullOrEmpty(record.Value) ? _imageService.GetImageBaseUrl() + record.Value : string.Empty : record.Value;
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

        [HttpGet("GetUserMetaData")]
        public async Task<IActionResult> GetUserMetaDataById([FromHeader] string userId)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userMetaDataRepository.GetUserMetaDataByIdAsync(userId);
                if (data != null)
                {
                    foreach (var record in data)
                    {
                        record.Value = record.FieldType == "Image" ? !string.IsNullOrEmpty(record.Value) ? _imageService.GetImageBaseUrl() + record.Value : string.Empty : record.Value;
                    }
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested User Metadata for Id = {userId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        //[HttpPost("")]
        //public async Task<IActionResult> AddUserMetaData([FromForm] IFormCollection model, [FromHeader] string userId)
        //{
        //    SCRMResponse response = new();
        //    try
        //    {
        //        var data = await _userMetaDataRepository.AddUserMetaDataAsync(model, userId);
        //        if (data != null)
        //        {
        //            response.success = true;
        //            response.data = data;
        //            return Created(nameof(AddUserMetaData), response);
        //        }
        //        response.error = "Please Enter Required Data for Addition of User MetaData";
        //        return StatusCode(204, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.error = ex.Message;
        //        return StatusCode(500, response);
        //    }
        //}

        [HttpPost("SaveUserMetaData")]
        public async Task<IActionResult> SaveUserMetaData([FromForm] IFormCollection model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userMetaDataRepository.AddUpdateUserMetaDataAsync(model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = "Please Enter Required Data for Addition of User MetaData";
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUserMetaData([FromHeader] string userId, [FromForm] IFormCollection model)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _userMetaDataRepository.UpdateUserMetaDataAsync(userId, model);
                if (data != null)
                {
                    response.success = true;
                    response.data = data;
                    return Ok(response);
                }
                response.error = $"Requested User Metadata for Id = {userId} is Not Found...!";
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
