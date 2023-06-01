using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BOTAnalyticsController : ControllerBase
    {
        private readonly IBOTAnalyticsRepository _AnalyticsRepo;
        public BOTAnalyticsController(IBOTAnalyticsRepository analyticsRepo)
        {
            _AnalyticsRepo = analyticsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInteraction(Guid VisitorUUId)
        {
            BOTResponse response = new();
            try
            {
                var Chat = await _AnalyticsRepo.GetVisitorChatAsync(VisitorUUId);
                response = response.ActionResultData(Chat, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetLeadsByBot(long botId, [Required] int pageNumber)
        {
            BOTResponse response = new();
            try
            {
                var Leads = await _AnalyticsRepo.GetLeadsByBotAsync(botId, pageNumber);
                response = response.ActionResultData(Leads, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);

        }

        [HttpGet]
        public async Task<IActionResult> GetLeadsByUser([Required] int pageNumber)
        {
            BOTResponse response = new();
            try
            {
                var Leads = await _AnalyticsRepo.GetLeadsByUserAsync(pageNumber);
                response = response.ActionResultData(Leads, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }
    }
}
