using Amazon.Runtime.Internal.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NPOI.Util;
using System.Globalization;
using System.Net.Http.Headers;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Enums;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.Utility;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.BLL.Services
{
    public class WebHookService : IWebHookService
    {
        private readonly IBOTGetflowService _workflowRepo;
        private readonly IBotWhatsAppRepository _whatsAppRepo;
        private readonly IBOTchatbotService _chatbot;
        private readonly IWebHookRepository _webhook;
        private readonly IBOTVisitorRepository _visitor;
        private readonly IBOTWhatsAppChannel _whatsApp;
        private readonly IBOTHistoryRepository _history;
        private readonly IBotQuestionRepository _question;
        private readonly IBOTWhatsAppTemplateRepository _template;
        private readonly IBOTComponentRepository _component;

        public WebHookService(IBOTGetflowService workflowRepo, IBotWhatsAppRepository whatsAppRepo,IBOTVisitorRepository visitor,IBOTWhatsAppChannel whatsApp, IBotQuestionRepository question, IBOTWhatsAppTemplateRepository template,IBOTComponentRepository component, IBOTchatbotService chatbot,IWebHookRepository webhook,IBOTHistoryRepository history) 
        {
            _workflowRepo = workflowRepo;
            _whatsAppRepo = whatsAppRepo;
            _visitor = visitor; 
            _whatsApp = whatsApp;
            _question = question;   
            _template = template;
            _component = component;
            _chatbot = chatbot;
            _webhook = webhook;
            _history = history;
        }

        public async Task<string?> HandleWebhookRequest(BOTWebhookModel model)
        {
            var fileName = "Response.txt";
            string FolderPath = DirectoryConfig.Get(AppDirectory.BotResponse);
            if (!File.Exists(FolderPath + fileName))
                File.Create(FolderPath + fileName).Close();

            StreamWriter stream = new StreamWriter(FolderPath + fileName, true);
            stream.WriteLine(JsonConvert.SerializeObject(model));
            stream.Close();

            BOTWebHookResponse res;
            if (model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData == null)
            {
                res = new BOTWebHookResponse
                {
                    BussinessId = model.Entry!.FirstOrDefault()?.Id!,
                    PhoneNumberId = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Metadata!.PhoneNumberId!,
                    From = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.From!,
                    ResponseBody = JsonConvert.SerializeObject(model),
                    To = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Metadata!.DisplayPhoneNumber,
                    TimeStamp = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.Timestamp
                };

                if (model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.Type == "text")
                {
                    res.Message = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.Text!.Body;
                }
                else if (model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.Type == "button")
                {
                    res.Message = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.Messages!.FirstOrDefault()?.Button!.Text;
                }
            }
            else
            {
                res = new BOTWebHookResponse
                {
                    BussinessId = model.Entry!.FirstOrDefault()?.Id!,
                    PhoneNumberId = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.PhoneNumberId!,
                    From = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.From!,
                    ResponseBody = JsonConvert.SerializeObject(model),
                    To = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.DisplayPhoneNumber,
                    TimeStamp = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.Timestamp
                };
                if (model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.Type == "text")
                {
                    res.Message = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.Text!.Body;
                }
                else if (model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.Type == "button")
                {
                    res.Message = model.Entry!.FirstOrDefault()?.Changes!.FirstOrDefault()?.Value!.WhatsappBusinessApiData!.Messages!.FirstOrDefault()?.Button!.Text;
                }
            }
            var response = await _webhook.GetResponseByFromAsync(res.From);
            var Account = await _whatsAppRepo.GetWABAByBussinessIdAsync(res.BussinessId);
            var QuestionFlow = await _workflowRepo.GetFlow(Account!.ChatBotId);
            if (response== null)
            {
                if (res.Message != null)
                {
                    BOTVisitor visitor = new BOTVisitor()
                    {
                        VisitorUUId = Guid.NewGuid(),
                        Platform = BOTChannels.WhatsApp,
                    };
                    var visitors = await _visitor.AddVisitorAsync(visitor);
                    res.BOTVisitorsId = visitors.Id;
                    long VisitorId = (long)await FirstWebHookMessage(res, QuestionFlow!.OrderBy(x => x.Sequence).ToList(), visitors);
                    await _webhook.AddResponseAsync(res);
                }
            }
            else
            {
                //long VisitorId = (long)await FirstWebHookMessage(res, QuestionFlow!.OrderBy(x => x.Sequence).ToList(), visitorss);
                await FlowWebHook(response!,res);
            }   
            
            return StatusCodes.Status200OK.ToString();
        }

        private async Task<long?> FirstWebHookMessage(BOTWebHookResponse response, List<BOTQuestionViewModel> questions, BOTVisitor visitor)
        {
            ////Session Tracker
            //var comMessage = await _component.GetComponentByLabelAsync("message");
            //var comImage = await _component.GetComponentByLabelAsync("image");
            //var comLink = await _component.GetComponentByLabelAsync("link");
            //var comFile = await _component.GetComponentByLabelAsync("file");
            var botcomponent = await _component.GetMessageLinkImageFile();
            var component = botcomponent.Select(x => x.Id).ToList();
            var question = new BOTQuestion()
            {
                Id = (long)questions.FirstOrDefault()?.Id!,
                ChatBotId = (long)(questions.FirstOrDefault()?.ChatBotId)!,
                Question = questions.FirstOrDefault()?.Question!,
                ComponentTypeId = (long)questions.FirstOrDefault()?.ComponentTypeId!,
                Target = questions.FirstOrDefault()?.Target!
            };
            //while(component.Contains(question!.ComponentTypeId))
            //{
            //    if()
            //}
            while (question!.ComponentTypeId == botcomponent.Where(x=>x.Label == "Message").Select(x=>x.Id).FirstOrDefault())
            {
                BOTHistoryModel model = new BOTHistoryModel()
                {
                    ChatBotId = question.ChatBotId,
                    QuestionId = (long)question.Id!,
                    Reply = null,
                    VisitorUUId = visitor.VisitorUUId
                };
                await _chatbot.SaveReplyAsync(model);
                var templates = await _template.GetWhatsAppTemplatesByQueID((long)question.Id!);
                await _whatsApp.SendWhatsAppMessageAsync(templates!.WhatsAppTemplateId, response.From!);
                //await _whatsApp.SendWhatsAppMessageAsync("1178800316092730", "918780500537");
                question = question!.Target != "-1" ? await _question.GetQuestionByTargetId(question!.Target, question.ChatBotId) : new BOTQuestion();
            }
            var template = await _template.GetWhatsAppTemplatesByQueID((long)question.Id!);
            if(template!=null)
                await _whatsApp.SendWhatsAppMessageAsync(template!.WhatsAppTemplateId, response.From!);
            //await _whatsApp.SendWhatsAppMessageAsync("1558983171263523", "918780500537");
            return visitor.Id;
        }

        private async Task<string?> FlowWebHook(BOTWebHookResponse response, BOTWebHookResponse res)
        {
            var comMessage = await _component.GetComponentByLabelAsync("message");
            var botcomponent = await _component.GetMessageLinkImageFile();
            var component = botcomponent.Select(x => x.Id).ToList();
            var LastQuestion = await _history.GetHistoryByVisitorId(response.BOTVisitorsId);
            //var ResQue = await _question.GetQuestionComponentById(LastQuestion!.QuestionId);
            var ResQuestion = await _question.GetQuestionByTargetId(LastQuestion!.Question!.Target, LastQuestion.Question.ChatBotId);
            var visitor = await _visitor.GetBotVisitorById(response.BOTVisitorsId);
            BOTHistoryModel model = new BOTHistoryModel()
            {
                ChatBotId = ResQuestion!.ChatBotId,
                QuestionId = (long)ResQuestion.Id!,
                Reply = res.Message,
                VisitorUUId = visitor!.VisitorUUId
            };
            var validreply = await _chatbot.SaveReplyAsync(model);
            if(validreply != null)
            {
                BOTQuestion NextQue;

                if (ResQuestion.Options!.Count != 0)
                {
                    var targetOption = ResQuestion.Options.Where(x => x.Value == res.Message && x.QuestionId == ResQuestion.Id).Select(x => x.Target).FirstOrDefault();
                    NextQue = await _question.GetQuestionByTargetId(targetOption, ResQuestion.ChatBotId);
                }
                else
                {
                    NextQue = ResQuestion!.Target != "-1" ? await _question.GetQuestionByTargetId(ResQuestion!.Target, ResQuestion.ChatBotId) : new BOTQuestion(); // Here, You have to add validation template message Id
                }

                //while(component.Contains(question!.ComponentTypeId))
                //{
                //    if()
                //}
                while (NextQue!= null && NextQue!.ComponentTypeId == comMessage!.Id)
                {
                    BOTHistoryModel models = new BOTHistoryModel()
                    {
                        ChatBotId = NextQue.ChatBotId,
                        QuestionId = (long)NextQue.Id!,
                        Reply = null,
                        VisitorUUId = visitor!.VisitorUUId
                    };
                    await _chatbot.SaveReplyAsync(models);
                    var temp = await _template.GetWhatsAppTemplatesByQueID((long)NextQue.Id!);
                    if(temp != null)
                        await _whatsApp.SendWhatsAppMessageAsync(temp!.WhatsAppTemplateId, response.From!);
                    //await _whatsApp.SendWhatsAppMessageAsync("1178800316092730", "918780500537");
                    NextQue = NextQue!.Target != "-1" ? await _question.GetQuestionByTargetId(NextQue!.Target, NextQue.ChatBotId) : new BOTQuestion();
                    //Check the Target Id
                }
                if(NextQue!=null)
                {
                    var templates = await _template.GetWhatsAppTemplatesByQueID(NextQue!.Id!);
                    if(templates!=null)
                        await _whatsApp.SendWhatsAppMessageAsync(templates!.WhatsAppTemplateId, response.From!);
                }
                //await _whatsApp.SendWhatsAppMessageAsync("1558983171263523", "918780500537");
            }
            else
            {
                var errorMessage = await _whatsAppRepo.GetWABAByBussinessIdAsync(response.BussinessId!);
                BOTSendWhatsAppTemplate templates = new BOTSendWhatsAppTemplate()
                {
                    To = response.From!,
                    Template = new Template()
                    {
                        Name = "Error_message".ToLower(),
                        Language = new Language()
                        {
                            Code = "en"
                        }
                    }
                };
                string responseContent = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://graph.facebook.com/v15.0/{errorMessage!.PhNoId}/messages"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + errorMessage.WAToken);

                        request.Content = new StringContent(JsonConvert.SerializeObject(templates));
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var sent = await httpClient.SendAsync(request);
                    }
                }

            }
            return StatusCodes.Status200OK.ToString();
        }

    }
}
