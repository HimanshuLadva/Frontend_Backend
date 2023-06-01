using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.POIFS.Crypt.Dsig;
using RestSharp;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Enums;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;
using System.Text.Json;
using WebsiteCMS.BLL.Interfaces;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BOTChatController : ControllerBase
    {
        private readonly IBOTchatbotService _BotService;
        private readonly IBOTWebAppChannelService _webapp;
        private readonly IBOTWhatsAppChannel _whatsapp;
        private readonly IWhatsAppService _whatsAppServise;

        public BOTChatController(IBOTchatbotService botRepo, IBOTWhatsAppChannel whatsapp, IWhatsAppService whatsAppServise, IBOTWebAppChannelService webapp)
        {
            _BotService = botRepo;
            _whatsapp = whatsapp;
            _whatsAppServise = whatsAppServise;
            _webapp = webapp;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBot([FromForm] BOTChatBotModel model)
        {
            BOTResponse response = new();
            if (ModelState.IsValid)
            {
                try
                {
                    var botId = await _BotService.CreateBotAsync(model);
                    response = response.ActionResultData(new { id = botId }, StatusCodes.Status201Created);
                }
                catch (CustomDBException e)
                {
                    await e.LogException(ControllerContext);
                    response = response.ExceptionResult(e);
                }
            }
            else
            {
                response = response.ActionResultData(null, StatusCodes.Status400BadRequest);
            }
            return StatusCode(response.status, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBot(long BotId)
        {
            BOTResponse response = new();
            try
            {
                var Id = await _BotService.DeleteBotAsync(BotId);
                response = response.ActionResultData(new { id = Id }, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBotList()
        {
            BOTResponse response = new();
            try
            {
                var bots = await _BotService.GetBotListAsync();
                response = response.ActionResultData(bots ?? new List<BOTChatBotModel>(), StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveUserReply(BOTHistoryModel? model)
        {
            BOTResponse response = new();
            try
            {
                var result = new object();
                var CurrChannel = model!.GetType().ToString().ToLower();
                switch (CurrChannel)
                {
                    case BOTChannels.WhatsApp:
                        result = null;
                        break;

                    case BOTChannels.Telegram:
                        result = null;
                        break;

                    default:
                        result = await _webapp.Execute(model);
                        break;
                }
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> StartBot(long BotId, Guid? session = null)
        {
            //var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value + "/";
            BOTResponse response = new();
            try
            {
                Guid UniqueSession = (Guid)(session == null ? Guid.NewGuid() : session);
                var result = await _BotService.StartBot(BotId, UniqueSession);

                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        #region WhatsAppIntegrationAPIs

        [HttpPost]
        public async Task<IActionResult> RegisterBotToWhatsApp(BOTWhatsAppBusinessDataModel model)
        {
            BOTResponse response = new();
            try
            {
                var result = await _whatsAppServise.RegisterBotOnWhatsAppAsync(model);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMessageTemplate(BOTWhatsappMessageTemplateModel model)
        {
            BOTResponse response = new();
            try
            {
                RestResponse result = await _whatsAppServise.RegisterTemplateAsync(model);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = response.ActionResultData(result.Content, (int)result.StatusCode);
                }
                else
                {
                    response = response.ActionResultData(result.ErrorMessage, (int)result.StatusCode);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpPost]
        public async Task<IActionResult> EditTemplate(ICollection<JObject> Components, string message_template_id)
        {
            BOTResponse response = new();
            try
            {
                RestResponse result = await _whatsapp.EditTemplateAsync(Components, message_template_id);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = response.ActionResultData(result.Content, (int)result.StatusCode);
                }
                else
                {
                    response = response.ActionResultData(result.ErrorMessage, (int)result.StatusCode);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }


        [HttpGet]
        public async Task<IActionResult> GetTemplate(string bussinessId)
        {
            BOTResponse response = new();
            try
            {
                var result = await _whatsapp.GetTemplateAsync(bussinessId);
                if (result.StatusCode == 200)
                {
                    response = response.ActionResultData(result.Value, (int)result.StatusCode);
                }
                else
                {
                    response = response.ActionResultData(null, (int)result.StatusCode!);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplateStatusByID(string TemplateId, long chatbotId)
        {
            BOTResponse response = new();
            try
            {
                var result = await _whatsAppServise.GetTemplateStatusAsync(TemplateId, chatbotId);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTemplate(string TemplateName)
        {
            BOTResponse response = new();
            try
            {
                var result = await _whatsapp.DeleteTemplateAsync(TemplateName);
                response = response.ActionResultData(result.Content, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string id, string phonenumber)
        {
            BOTResponse response = new();
            try
            {
                string result = await _whatsapp.SendWhatsAppMessageAsync(id, phonenumber);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterBotFlows(long botID)
        {
            BOTResponse response = new();
            try
            {
                var result = await _whatsAppServise.RegisterBotFlow(botID);
                if (result == "200")
                {
                    response = response.ActionResultData("Successfully registered", StatusCodes.Status200OK);
                }
                else
                {
                    response = response.ActionResultData("Issue", StatusCodes.Status400BadRequest);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

    }

    #endregion WhatsAppIntegrationAPIs
}