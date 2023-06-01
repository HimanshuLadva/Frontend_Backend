using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class WhatsAppService : IWhatsAppService
    {

        private readonly IBOTGetflowService _getflowRepo;
        private readonly IBOTChatBOTRepository _botRepo;
        private readonly IBotWhatsAppRepository _botWhatsAppRepo;
        private readonly IBotQuestionRepository _question;
        private readonly IBotWhatsApTempStatusRepository _statusRepo;
        private readonly IBOTWhatsAppTemplateRepository _whatTemp;
        private readonly IBOTWhatsAppTempRegisterIssus _TempIssue;
        private readonly IBOTImageOrFileRepository _imagefile;
        private readonly IMapper _Mapper;
        private static Random random = new Random();
        public WhatsAppService(IBOTGetflowService getflowRepo, IBOTWhatsAppTempRegisterIssus TempIssue, IBOTWhatsAppTemplateRepository whatTemp, IBOTChatBOTRepository botRepo, IBotWhatsAppRepository botWhatsApp, IMapper mapper, IBotWhatsApTempStatusRepository statusRepo, IBotQuestionRepository question, IBOTImageOrFileRepository imagefile)
        {
            _getflowRepo = getflowRepo;
            _botRepo = botRepo;
            _Mapper = mapper;
            _botWhatsAppRepo = botWhatsApp;
            _statusRepo = statusRepo;
            _TempIssue = TempIssue;
            _whatTemp = whatTemp;
            _question = question;
            _imagefile = imagefile;
        }

        public async Task<BOTWhatsAppBusinessDataModel> RegisterBotOnWhatsAppAsync(BOTWhatsAppBusinessDataModel model)
        {
            var bot = await _botRepo.GetBotByIdAsync(model.ChatBotId);
            var errors = new List<Component>();
            Component header = new Component
            {
                type = "HEADER",
                format = "TEXT",
                text = bot!.Name
            };
            errors.Add(header);
            Component body = new Component
            {
                type = "BODY",
                text = "Please, Enter valid answer according Question."
            };
            errors.Add(body);
            Component footer = new Component
            {
                type = "FOOTER",
                text = "Weybee SocialSparsh"
            };
            errors.Add(footer);
            BOTWhatsappMessageTemplateModel errorModel = new BOTWhatsappMessageTemplateModel
            {
                Name = "Error_message",
                Language = "en",
                Category = "Marketing",
                Components = errors,
                ChatBotId = model.ChatBotId,
                QuestionId = -1
            };
            var components = JsonConvert.SerializeObject(errors).ToString().Replace("\r\n", string.Empty);
            components = Regex.Replace(components, @"\s\s+", string.Empty);
            var client = new RestClient();
            var request = new RestRequest($"https://graph.facebook.com/v15.0/{model.BusinessId}/message_templates", Method.Post);
            request.AddParameter("name", errorModel.Name.ToLower(), ParameterType.QueryString);
            request.AddParameter("language", errorModel.Language, ParameterType.QueryString);
            request.AddParameter("category", errorModel.Category.ToUpper(), ParameterType.QueryString);
            request.AddParameter("components", components, ParameterType.QueryString);
            request.AddParameter("access_token", model.WAToken, ParameterType.QueryString);
           
            RestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Regex getID = new Regex(@"((id))[^a-zA-Z0-9]+(\w+)");
                model.errorMessageID = getID.Match(result.Content!).Groups[3].Value.ToString();
            }
            else
            {
                model.errorMessageID = "59867403";
            }
            var BusinessAcc = _Mapper.Map<BOTWhatsAppBusinessData>(model);
            var record = await _botWhatsAppRepo.AddWABAAsync(BusinessAcc);
            return _Mapper.Map<BOTWhatsAppBusinessDataModel>(record);
        }

        public async Task<string> GetTemplateStatusAsync(string tempId, long chatbotID)
        {
            var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _botWhatsAppRepo.getWABAByChatBotID(chatbotID));

            var client = new RestClient();
            var request = new RestRequest($"https://graph.facebook.com/v15.0/{Account.BusinessId}/message_templates", Method.Get);
            request.AddParameter("access_token", Account.WAToken, ParameterType.QueryString);

            RestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return StatusCodes.Status400BadRequest.ToString();
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Regex getStatusRegex = new Regex(@"(status)[^a-zA-Z0-9]+(\w+)[^a-zA-Z0-9]+\w+[^a-zA-Z0-9]+\w+[^a-zA-Z0-9]+(id)[^a-zA-Z0-9]+(\w+)");
                MatchCollection matchedId = getStatusRegex.Matches(result.Content!);

                for (int count = 0; count < matchedId.Count; count++)
                {
                    var TempId = matchedId[count].Groups[4].Value;
                    var Status = matchedId[count].Groups[2].Value;
                    var templates = await _statusRepo.getStatusByTempId(TempId);
                    if (templates != null)
                    {
                        templates.Status = Status;
                        await _statusRepo.UpdateTemplateStatusAsync(templates);
                    }
                    else
                    {
                        BOTWhatsAppTemplatesStatus temp = new BOTWhatsAppTemplatesStatus { WhatsAppTemplateId = TempId, Status = Status };
                        await _statusRepo.AddTemplateStatusAsync(temp);
                    }
                }
            }

            var sts = _statusRepo.getStatusByTempId(tempId).Result!.Status;
            if (sts != null)
            {
                return sts;
            }
            else
            {
                return "No Data Found";
            }
        }

        public async Task<RestResponse> RegisterTemplateAsync(BOTWhatsappMessageTemplateModel model)
        {

            var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _botWhatsAppRepo.getWABAByChatBotID(model.ChatBotId));

            var components = JsonConvert.SerializeObject(model.Components).ToString().Replace("\r\n", string.Empty);
            components = Regex.Replace(components, @"\s\s+", string.Empty);

            var client = new RestClient();
            var request = new RestRequest($"https://graph.facebook.com/v15.0/{Account.BusinessId}/message_templates", Method.Post);
            request.AddParameter("name", model.Name.ToLower(), ParameterType.QueryString);
            request.AddParameter("language", model.Language, ParameterType.QueryString);
            request.AddParameter("category", model.Category.ToUpper(), ParameterType.QueryString);
            request.AddParameter("components", components, ParameterType.QueryString);
            request.AddParameter("access_token", Account.WAToken, ParameterType.QueryString);

            RestResponse result = await client.ExecuteAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Regex getID = new Regex(@"((id))[^a-zA-Z0-9]+(\w+)");
                var id = getID.Match(result.Content!).Groups[3].Value.ToString();
                BOTWhatsAppTemplates t = new BOTWhatsAppTemplates { QuestionId = model.QuestionId, WhatsAppTemplateId = id, WhatsAppTemplateName = model.Name.ToLower(), Language = model.Language };
                await _whatTemp.AddWhatsappTemplate(t);
            }
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                BOTWhatsAppTemplateRegisterIssue issue = new BOTWhatsAppTemplateRegisterIssue { Issue = result.Content! };
                await _TempIssue.AddIssue(issue);
            }
            return result;
        }

        public async Task<string> RegisterBotFlow(long BotId)
        {
            Dictionary<long, string> data = new Dictionary<long, string>();
            //var QueList = await _getflowRepo.GetFlow(BotId);
            var QueList = await _question.GetQuestionOptionComponent(BotId);
            var Bot = _botRepo.GetBotByIdAsync(BotId).Result!.Name;
            for (int i = 0; i < QueList!.Count(); i++)
            {
                var comList = new List<Component>();

                Component header = new Component
                {
                    type = "HEADER",
                };

                var imagepath = await _imagefile.GetImageOrFileByFrontendId(QueList[i].FrontendId);
                //List<string> headerhandle = new List<string>();
                //headerhandle.Add("data:image/jpeg;base64,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");
                //if (imagepath != null) headerhandle.Add(imagepath.ImageOrFilePath!);
                if (QueList[i].ComponentType.Label == "Image")
                {
                    header.format = "IMAGE";
                    //header.example = new Example()
                    //{
                    //    header_handle = headerhandle
                    //};
                }
                else if(QueList[i].ComponentType.Label == "File")
                {
                    header.format = "DOCUMENT";
                }
                else
                {
                    header.format = "TEXT";
                    header.text = Bot;
                }
                comList.Add(header);
                Component body = new Component
                {
                    type = "BODY",
                    text = QueList[i].Question
                };
                comList.Add(body);
                Component footer = new Component
                {
                    type = "FOOTER",
                    text = "Weybee SocialSparsh"
                };
                comList.Add(footer);
                if (QueList[i].Options!.Count != 0)
                {
                    Component button = new Component()
                    {
                        type = "BUTTONS"
                    };
                    button.buttons = new List<Buttons>();
                    foreach (var o in QueList[i].Options!)
                    {
                        button.buttons.Add(new Buttons()
                        {
                            type = "QUICK_REPLY",
                            text = o.Value
                        });
                    }
                    comList.Add(button);
                }
                else if (QueList[i].Links!.Count != 0)
                {
                    Component button = new Component()
                    {
                        type = "BUTTONS"
                    };
                    button.buttons = new List<Buttons>();
                    foreach (var L in QueList[i].Links!)
                    {
                        button.buttons.Add(new Buttons()
                        {
                            type = "URL",
                            text = L.LinkTitle,
                            url = L.LinkUrl
                        });
                    }
                    comList.Add(button);
                }
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var str = new string(Enumerable.Repeat(chars, 7)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                BOTWhatsappMessageTemplateModel model = new BOTWhatsappMessageTemplateModel
                {
                    Name = str,
                    Language = "en",
                    Components = comList,
                    ChatBotId = BotId,
                    QuestionId = (long)QueList[i].Id!
                };
                var c = await RegisterTemplateAsync(model);
            }

            return "200";
        }
    }
}
