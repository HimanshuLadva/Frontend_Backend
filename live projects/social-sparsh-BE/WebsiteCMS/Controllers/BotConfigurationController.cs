using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NPOI.POIFS.Crypt.Dsig;
using System.Security.Claims;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class BOTConfigurationController : Controller
    {
        private readonly IBOTGetflowService _Workflow;

        public BOTConfigurationController(IBOTGetflowService Workflow)
        {
            _Workflow = Workflow;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlow([FromRoute] long id)
        {
            BOTResponse response = new();
            try
            {
                var temp = await _Workflow.GetFlow(id);
                response = response.ActionResultData(temp ?? new List<BOTQuestionViewModel>(), StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpPut]
        public async Task<IActionResult> EditFlow(BOTChatBotModel model)
        {
            BOTResponse response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _Workflow.editFlow(model);
                    response = response.ActionResultData(result, StatusCodes.Status200OK);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBotById([FromRoute] long id)
        {

            BOTResponse response = new();
            try
            {
                var result = await _Workflow.GetBotById(id);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
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