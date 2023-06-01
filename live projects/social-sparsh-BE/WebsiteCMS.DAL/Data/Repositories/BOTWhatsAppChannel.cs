using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.OpenXmlFormats;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using NPOI.XSSF.UserModel.Helpers;
using NPOI.XWPF.UserModel;
using RestSharp;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTWhatsAppChannel : IBOTChannelSelector<BOTWhatsappMessageTemplateModel>, IBOTWhatsAppChannel
    {
        private readonly WebsiteCMSDbContext _Context;
        private readonly IMapper _Mapper;
        private readonly IBOTImageOrFileRepository _imagefile;
        private static Random random = new Random();

        public BOTWhatsAppChannel(WebsiteCMSDbContext context, IMapper mapper, IBOTImageOrFileRepository imagefile)
        {
            _Context = context;
            _Mapper = mapper;
            _imagefile = imagefile;
        }

        public Task<object> Execute(BOTWhatsappMessageTemplateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<object> GenerateResponse(BOTWhatsappMessageTemplateModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendWhatsAppMessageAsync(string id, string phoneNumber)
        {
            var tempInfo = _Context.tblBOTWhatsAppTemplates.Where(x => x.WhatsAppTemplateId == id).Include(x=>x.Question).ThenInclude(x=>x.ComponentType).FirstOrDefault()!;
            var chatBot = _Context.tblBOTQuestion.Where(x => x.Id == tempInfo!.QuestionId).Select(x => x.ChatBotId).First();
            var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _Context.tblBOTWhatsAppBusinessData.Where(x => x.ChatBotId == chatBot).FirstOrDefaultAsync());
           // var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _Context.tblBOTWhatsAppBusinessData.Where(x => x.ChatBotId == 12).FirstOrDefaultAsync());
            BOTSendWhatsAppTemplate templates = new BOTSendWhatsAppTemplate()
            {
                To = phoneNumber,
                //To = "918780500537",
                Template = new Template()
                {
                    Name = tempInfo.WhatsAppTemplateName,
                   // Name = "hello_world",
                    Language = new Language()
                    {
                        Code = tempInfo.Language
                        //Code = "en_US"
                    }
                }
            };


            var imagepath = await _imagefile.GetImageOrFileByFrontendId(tempInfo.Question.FrontendId);
            if (imagepath != null)
            {
                templates.Template.Components!.Add(new Components()
                {
                    Type = "header"
                });
                if (tempInfo.Question.ComponentType.Label == "Iamge")
                {
                    templates.Template.Components!.First().Parameters!.Add(new DAL.Models.Parameter()
                    {
                        Type = "image",
                        Image = new Images()
                        {
                            Link = imagepath.ImageOrFilePath!
                        }
                    });
                }
                else if (tempInfo.Question.ComponentType.Label == "File")
                {
                    templates.Template.Components!.First().Parameters!.Add(new DAL.Models.Parameter()
                    {
                        Type = "document",
                        Document = new Documents()
                        {
                            Link = imagepath.ImageOrFilePath!
                        }
                    });
                }
            }


            string responseContent = string.Empty;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://graph.facebook.com/v15.0/{Account.PhNoId}/messages"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Account.WAToken);

                    request.Content = new StringContent(JsonConvert.SerializeObject(templates));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode == true)
                    {
                        responseContent = "Message sent successfully on " + phoneNumber;
                    }
                    else
                    {
                        responseContent = response.StatusCode.ToString()!;
                    }
                }
            }
            return responseContent;
        }

        public async Task<RestResponse> EditTemplateAsync(ICollection<JObject> Components, string message_template_id)
        {
            try
            {
                var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _Context.tblBOTWhatsAppBusinessData.Where(x => x.ChatBotId == 1).FirstOrDefaultAsync());

                var components = JsonConvert.SerializeObject(Components).ToString().Replace("\r\n", string.Empty);
                components = Regex.Replace(components, @"\s\s+", string.Empty);

                var client = new RestClient();
                var request = new RestRequest($"https://graph.facebook.com/v15.0/{message_template_id}", Method.Post);
                request.AddParameter("components", components, ParameterType.QueryString);
                request.AddParameter("access_token", Account.WAToken, ParameterType.QueryString);

                RestResponse result = await client.ExecuteAsync(request);

                return result;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }

        public async Task<ObjectResult> GetTemplateAsync(string bussinessId)
        {
            try
            {
                var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _Context.tblBOTWhatsAppBusinessData.Where(x => x.BusinessId == bussinessId).FirstOrDefaultAsync());

                var client = new RestClient();
                var request = new RestRequest($"https://graph.facebook.com/v15.0/{Account.BusinessId}/message_templates", Method.Get);
                request.AddParameter("access_token", Account.WAToken, ParameterType.QueryString);

                RestResponse result = await client.ExecuteAsync(request);

                var responses = JsonConvert.DeserializeObject(result.Content!);
                var results = new ObjectResult(responses);
                results.StatusCode = (int?)result.StatusCode;
                return results;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }

        public async Task<RestResponse> DeleteTemplateAsync(string templateName)
        {
            try
            {
                var Account = _Mapper.Map<BOTWhatsAppBusinessDataModel>(await _Context.tblBOTWhatsAppBusinessData.Where(x => x.ChatBotId == 1).FirstOrDefaultAsync());

                var client = new RestClient();
                var request = new RestRequest($"https://graph.facebook.com/v15.0/{Account.BusinessId}/message_templates", Method.Delete);
                request.AddParameter("access_token", Account.WAToken, ParameterType.QueryString);
                request.AddParameter("name", templateName, ParameterType.QueryString);

                RestResponse result = await client.ExecuteAsync(request);
                return result;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }


    }
}