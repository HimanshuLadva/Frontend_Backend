using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HPSF;
using System.Net;
using System.Net.Http.Headers;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.Controllers
{
    [ApiController]
    public class BOTWebHookController : ControllerBase
    {
        private readonly IWebHookService _webhook;

        public BOTWebHookController(IWebHookService webhook)
        {
            _webhook = webhook;
        }

        [AllowAnonymous]
        [HttpGet("whatsapp_hook")]
        public IActionResult recognize_token()
        {
            string mode = Request.Query["hub.mode"].ToString();
            string challenge = Request.Query["hub.challenge"].ToString();
            string token = Request.Query["hub.verify_token"].ToString();

            if (mode == "subscribe" && token == "WeyBeeSocial1234")
            {
                Response.StatusCode = 200;
                return Ok(challenge);
            }
            else
            {
                Response.StatusCode = 403;
                return BadRequest();
            }

        }

        [AllowAnonymous]
        [HttpPost("whatsapp_hook")]
        public async Task<IActionResult> HandleWebhookRequest([FromBody] BOTWebhookModel model)
        {
            BOTResponse response = new();
            try
            {
                var result = _webhook.HandleWebhookRequest(model);
                response = response.ActionResultData(result.Result, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }

        [AllowAnonymous]
        [HttpGet("File")]
        public async Task<string> WebHookResponseFile()
        {
            //It's Just FOR testing..
            BOTResponse response = new();
            try
            {
                using (StreamReader r = new StreamReader(@"C:\Chatbot\SocialSparsh\WebsiteCMS\Resources\BOT\Response.txt"))
                {
                    string json = r.ReadToEnd();
                    return json;
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return "Error";
        }

    }
}
