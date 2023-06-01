using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/UserTemplate")]
    [ApiController]
    [Authorize]
    public class SCRMUserTemplateController : ControllerBase
    {
        private readonly SCRMIUserTemplateRepository _userTemplateRepository;
        private readonly SCRMIUserRepository _userRepository;
        private readonly SCRMITemplateRepository _templateRepository;

        public SCRMUserTemplateController(SCRMIUserTemplateRepository userTemplateRepository, SCRMIUserRepository userRepository, SCRMITemplateRepository templateRepository)
        {
            _userTemplateRepository = userTemplateRepository;
            _userRepository = userRepository;
            _templateRepository = templateRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUserTemplate([FromHeader] string userId, [FromQuery] SCRMRequestParams requestParams)
        {
            var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
            SCRMResponse response = new();
            try
            {
                //var existUser = await _userRepository.GetUserByIdAsync(userId);
                //if (existUser != null)
                {
                    var data = await _userTemplateRepository.GetAllUserTemplateAsync(userId, requestParams);
                    if (data != null)
                    {
                        foreach (var userTemplate in data)
                        {
                            var premiumFolder = "/Resources/SCRM/Private/Images/TemplateImages/PremiumTemplateImages/";
                            var publicFolder = "/Resources/SCRM/Private/Images/TemplateImages/PublicTemplateImages/";
                            string folderSelection = userTemplate.IsFree ? premiumFolder : publicFolder;

                            userTemplate.TemplateImageURL = !string.IsNullOrEmpty(userTemplate.TemplateImageURL) ? baseURL + folderSelection + userTemplate.TemplateImageURL : string.Empty;
                            foreach (var field in userTemplate.ImageFields)
                            {
                                //field.Value = baseURL + field.Value;
                                field.Value = !string.IsNullOrEmpty(field.Value) ? baseURL + field.Value : string.Empty;
                            }
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
                response.error = $"Requested User Templates for Id = {userId} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{templateId}")]
        public async Task<IActionResult> GetUserTemplateById([FromHeader] string userId, [FromRoute] int templateId)
        {
            var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
            SCRMResponse response = new();
            try
            {
                //var existUser = await _userRepository.GetUserByIdAsync(userId);
                //if (existUser != null)
                {
                    var existTemplate = await _templateRepository.GetTemplateByIdAsync(templateId);
                    if (existTemplate != null)
                    {
                        var data = await _userTemplateRepository.GetUserTemplateByIdAsync(userId, templateId);

                        var premiumFolder = "/Resources/SCRM/Private/Images/TemplateImages/PremiumTemplateImages/";
                        var publicFolder = "/Resources/SCRM/Private/Images/TemplateImages/PublicTemplateImages/"; 
                        string folderSelection = data.IsFree ? premiumFolder : publicFolder;

                        data.TemplateImageURL = !string.IsNullOrEmpty(data.TemplateImageURL) ? baseURL + folderSelection + data.TemplateImageURL : string.Empty;
                        foreach (var record in data.ImageFields)
                        {
                            record.Value = !string.IsNullOrEmpty(record.Value) ? baseURL + record.Value : string.Empty;
                        }

                        if (data != null)
                        {
                            response.data = data;
                            response.success = true;
                            return Ok(response);
                        }
                    }
                    response.error = $"Requested Template for Id = {templateId} is Not Found...!";
                    return NotFound(response);
                }
                response.error = $"Requested User for Id = {userId} is Not Found...!";
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
