using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("SCRM/TemplateDownload")]
    [ApiController]
    [Authorize]
    public class SCRMTemplateDownloadController : ControllerBase
    {
        private readonly SCRMITemplateDownloadRepository _templateDownloadRepository;
        private readonly SCRMIUserRepository _userRepository;
        private readonly SCRMITemplateRepository _templateRepository;

        public SCRMTemplateDownloadController(SCRMITemplateDownloadRepository templateDownloadRepository, SCRMIUserRepository userRepository, SCRMITemplateRepository templateRepository)
        {
            _templateDownloadRepository = templateDownloadRepository;
            _userRepository = userRepository;
            _templateRepository = templateRepository;
        }

        [HttpGet("{templateId}")]
        public async Task<IActionResult> DownloadUserTemplateById([FromHeader] string userId, [FromHeader] float templateWidth, [FromHeader] float templateHeight, [FromRoute] int templateId)
        {
            IHeaderDictionary model = HttpContext.Request.Headers;
            SCRMResponse response = new();
            try
            {
                //var existUser = await _userRepository.GetUserByIdAsync(userId);
                //if (existUser != null)
                {
                    var existTemplate = await _templateRepository.GetTemplateByIdAsync(templateId);
                    if (existTemplate != null)
                    {
                        var data = await _templateDownloadRepository.DownloadUserTemplateByIdAsync(userId, templateId, templateWidth, templateHeight, model);
                        if (data != null)
                            return data;
                        else
                            response.error = $"Requested Template for Id = {templateId} can not be Proceed Due to Different Accept Ratio...!";
                        return NotFound(response);
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
