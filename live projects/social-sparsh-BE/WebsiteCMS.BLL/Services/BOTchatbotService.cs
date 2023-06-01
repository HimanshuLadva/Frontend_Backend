using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.BLL.Services
{
    /// <summary>
    ///      A <see cref="class"/> that contains methods and necessary fields to implement <see cref="IBOTchatbotService"/>
    /// </summary>
    public class BOTchatbotService : IBOTchatbotService
    {
        public readonly IBotQuestionRepository _queRepo;
        public readonly IBOTChatBOTRepository _chatbot;
        public readonly IBOTComponentRepository _component;
        public readonly IBOTVisitorRepository _visitor;
        public readonly IBOTHistoryRepository _history;
        public readonly IBOTSessionTracker _session;
        private readonly IAWSImageService _imageRepository;
        private readonly IBOTComponentRepository _componentRepository;
        private readonly IBOTImageOrFileRepository _imageOrFileRepository;
        public readonly IMapper _Mapper;
        private IBaseRepository _baseRepository;
        private I
            Accessor _httpContextAccessor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BOTchatbotService"/> class.
        /// </summary>
        /// <param name="queRepo">The que repo.</param>
        /// <param name="Mapper">The mapper.</param>
        /// <param name="chatbot">The chatbot.</param>
        /// <param name="baseRepository">The base repository.</param>
        /// <param name="component">The component.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="visitor">The visitor.</param>
        /// <param name="history">The history.</param>
        /// <param name="session">The session.</param>
        /// <param name="imageRepository">The image repository.</param>
        /// <param name="componentRepository">The component repository.</param>
        /// <param name="imageOrFileRepository">The image or file repository.</param>
        public BOTchatbotService(IBotQuestionRepository queRepo, IMapper Mapper, IBOTChatBOTRepository chatbot, IBaseRepository baseRepository, IBOTComponentRepository component, IHttpContextAccessor httpContextAccessor, IBOTVisitorRepository visitor, IBOTHistoryRepository history, IBOTSessionTracker session, IAWSImageService imageRepository, IBOTComponentRepository componentRepository, IBOTImageOrFileRepository imageOrFileRepository)
        {
            _queRepo = queRepo;
            _Mapper = Mapper;
            _chatbot = chatbot;
            _baseRepository = baseRepository;
            _component = component;
            _httpContextAccessor = httpContextAccessor;
            _visitor = visitor;
            _history = history;
            _session = session;
            _imageRepository = imageRepository;
            _componentRepository = componentRepository;
            _imageOrFileRepository = imageOrFileRepository;
        }

        public async Task<long> CreateBotAsync(BOTChatBotModel model)
        {
            model.ApplicationUserId = _baseRepository.GetUserId();
            await using var memoryStream = new MemoryStream();
            await model.AvtarImage!.CopyToAsync(memoryStream);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.AvtarImage!.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.BOTchatbot);
            model.Avatar = imageUrl;
            BOTChatBot bot = _Mapper.Map<BOTChatBot>(model);
            var comp = await _component.GetComponentByLabelAsync("message");
            BOTQuestion que = new BOTQuestion
            {
                ComponentTypeId = comp!.Id,
                Question = comp!.DefaultQuestion!,
                Sequence = 1,
                FrontendId = Guid.NewGuid().ToString(),
                Target = "0"
            };
            ICollection<BOTQuestion> questions = new List<BOTQuestion>();
            questions.Add(que);
            bot.Questions = questions;
            await _chatbot.AddBotAsync(bot);
            return bot.Id;
        }
        
        public async Task<long> DeleteBotAsync(long botId)
        {
            await _chatbot.DeleteBotAsync(botId);
            return botId;
        }
        
        public async Task<List<BOTChatBotModel>?> GetBotListAsync()
        {
            var bot = await _chatbot.GetBotListByUserIdAsync(_baseRepository.GetUserId());
            foreach(var b in bot)
            {
                b.Avatar = _imageRepository.GetImageBaseUrl() + b.Avatar;
            }
            if (bot.Count > 0) return _Mapper.Map<List<BOTChatBotModel>>(bot);
            else return new List<BOTChatBotModel>();
        }

        /// <summary>
        ///     Assigns the next question index.
        ///     <para>
        ///         Gets the object of type <see cref="BOTQuestionBaseViewModel"/>
        ///     </para>
        /// </summary>
        /// <param name="Obj">The object of type <see cref="BOTQuestionBaseViewModel"/>.</param>
        /// <param name="models">The models.</param>
        private static void AssignNextQuestionIndex<T>(T Obj, List<BOTQuestionViewModel> models)
            where T : BOTQuestionBaseViewModel
        {
            var target = models.Where(x => x.FrontendId == Obj.Target).FirstOrDefault();
            Obj.NextQuestionIndex = models.IndexOf(target!);
        }
        
        public List<BOTQuestionViewModel> UpdateIndex(List<BOTQuestionViewModel> models)
        {
            foreach (var model in models)
            {
                switch (model.Options!.Count)
                {
                    case 0:
                        AssignNextQuestionIndex(model, models);
                        break;
                    default:
                        {
                            model.Options.ToList().ForEach(x => AssignNextQuestionIndex(x, models));
                            break;
                        }
                }
            }
            return models;
        }

        public async Task<Dictionary<string, object>?> StartBot(long BotId, Guid session)
        {
            string? baseUrl = string.Empty;

            var bots = await _chatbot.GetBotFlow(BotId);
            var Que = await _queRepo.GetQuestionOptionComponent(BotId);
            BOTChatBot bot = new BOTChatBot
            {
                Id = bots!.Id,
                Avatar = bots.Avatar,
                Questions = Que,
                Platforms = bots.Platforms,
                Colour = bots.Colour,
                ApplicationUserId = bots.ApplicationUserId,
                DisplayName = bots.DisplayName,
                Name = bots.Name
            };
            if (bot != null)
            {
                var curRequest = _httpContextAccessor?.HttpContext?.Request;
                if (curRequest != null)
                {
                    baseUrl = $"{curRequest.Scheme + Uri.SchemeDelimiter + curRequest.Host.Value}" + "/";
                }
                var model = _Mapper.Map<BOTChatBotViewModel>(bot);
                model.Questions = UpdateIndex(model.Questions!.ToList());
                foreach (var item in model.Questions!)
                {
                    var component = _componentRepository.GetComponentByIdAsync(item.ComponentTypeId);
                    if (component.Result!.Label == "Image" || component.Result!.Label == "File")
                    {
                        var ImageOrFile = _imageOrFileRepository.GetImageOrFileByFrontendId(item.FrontendId);
                        item.ImageOrFilePath = _imageRepository.GetImageBaseUrl() + (ImageOrFile.Result.ImageOrFilePath != null ? ImageOrFile.Result.ImageOrFilePath : null);
                    }
                }
                model.Avatar = _imageRepository.GetImageBaseUrl() + bot.Avatar;
                var result = new Dictionary<string, object>
                {
                    { "ChatBot", model },
                    { "Visitor", await _session.GetVisitorSession(session) }
                };
                return result!;
            }
            return null;
        }
        
        public async Task<BOTHistoryModel?> SaveReplyAsync(BOTHistoryModel model)
        {
            var question = _Mapper.Map<BOTHistory>(model);
            if (model?.VisitorUUId != null) question.Visitor = await _visitor.GetBotVisitorByUUID((Guid)(model?.VisitorUUId));
            var component = await _queRepo.GetQuestionComponentById(model!.QuestionId);
            question.QuestionText = component!.Question;
            question.Reply = null;
            if (question != null)
            {
                var que = await _history.AddReplyAsync(question!);
            }
            if (model.Reply != null)
            {
                var reply = ValidateReply(component, model);
                if (reply != null)
                {
                    reply!.Visitor = question!.Visitor!;
                    reply!.QuestionText = question.QuestionText;
                    await _history.AddReplyAsync(reply);
                    return _Mapper.Map<BOTHistoryModel>(reply);
                }
                if (reply == null && component.ComponentType.Label.ToString().ToLower() != "message")
                {
                    await _history.DeleteLastQuesAsync(question!.Id);
                }
                return null;
            }
            return _Mapper.Map<BOTHistoryModel>(question);

            // Store valid and invalid both Reply from user, And Change Webhook acroding that.
        }

        /// <summary>
        ///     Validates the reply of the visitor weather it is valid or not.
        ///     <para>
        ///         Gets the <see cref="BOTQuestion"/> component.
        ///     </para>
        ///     <para>
        ///         Gets the <see cref="BOTHistoryModel"/> model.
        ///     </para>
        /// </summary>
        /// <param name="component">The <see cref="BOTQuestion"/> component.</param>
        /// <param name="model">The <see cref="BOTHistoryModel"/> model.</param>
        /// <returns>A <see cref="Nullable{T}"/> record of type <see cref="BOTHistory"/>.</returns>
        private BOTHistory? ValidateReply(BOTQuestion component, BOTHistoryModel model)
        {
            var question = _Mapper.Map<BOTHistory>(model);
            var type = component.ComponentType.Label.ToString().ToLower();
            bool matched = false;
            switch (type)
            {
                case "name":
                    matched = Regex.IsMatch(question.Reply!, @"^[a-zA-Z]{1,}\s?[a-zA-Z]{0,}$", RegexOptions.IgnoreCase);
                    break;
                case "email":
                    matched = Regex.IsMatch(question.Reply!, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    break;
                case "phone number":
                    matched = Regex.IsMatch(question.Reply!, @"\(?\d{2}\s?\d{1}?\)?\s?\-?\d{2}\s?\d{1}?\s?\-?\d{4}?$", RegexOptions.IgnoreCase);
                    break;
                default:
                    matched = true;
                    break;
            }
            if (matched)
            {
                question.IsBotMessage = false;
                return question;
            }
            return null;
        }


    }
}
