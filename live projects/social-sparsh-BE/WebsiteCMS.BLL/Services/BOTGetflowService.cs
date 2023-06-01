using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.BLL.Services
{
    public class BOTGetflowService : IBOTGetflowService
    {
        public readonly IBotQuestionRepository _queRepo;
        public readonly IBOTChatBOTRepository _chatbot;
        private readonly IBOTImageOrFileRepository _imageOrFileRepository;
        private readonly IBOTComponentRepository _componentRepository;
        private readonly IAWSImageService _imageService;
        public readonly IMapper _Mapper;
        private IBaseRepository _baseRepository;
        public BOTGetflowService(IBotQuestionRepository queRepo, IMapper Mapper, IBOTChatBOTRepository chatbot, IBOTImageOrFileRepository imageOrFileRepository, IBOTComponentRepository componentRepository, IAWSImageService imageService, IBaseRepository baseRepository)
        {
            _queRepo = queRepo;
            _Mapper = Mapper;
            _chatbot = chatbot;
            _imageOrFileRepository = imageOrFileRepository;
            _componentRepository = componentRepository;
            _imageService = imageService;
            _baseRepository = baseRepository;
        }

        private static void UpdateTargetId<T>(T Obj, List<BOTQuestion> Qlist, int i) where T : BOTQuestionBase
        {
            if (i < Qlist.Count - 1)
                Obj.Target = Qlist[i + 1].FrontendId;
            else
                Obj.Target = "-1";

        }

        private List<BOTQuestion>? updateflow(List<BOTQuestion> Questionlist)
        {
            if (Questionlist.Count != 0)
            {
                for (int i = 0; i < Questionlist.Count; i++)
                {
                    if (Questionlist[i].Target != "-1" && (Questionlist[i].Target == string.Empty || Questionlist[i].Target == null || Questionlist.Where(y => y.FrontendId == Questionlist[i].Target) == null || Questionlist[i].Target == "0"))
                    {
                        switch (Questionlist[i].Options!.Count)
                        {
                            case 0:
                                UpdateTargetId(Questionlist[i], Questionlist, i);
                                break;
                            default:
                                {
                                    List<BOTOption> options = Questionlist[i].Options!.ToList();
                                    options.ForEach(O =>
                                    {
                                        if (O.Target != "-1" && (O.Target == string.Empty || O.Target == null || Questionlist.Where(y => y.FrontendId == O.Target).FirstOrDefault() == null || O.Target == "0"))
                                        {
                                            UpdateTargetId(O, Questionlist, i);
                                        }
                                    });
                                }  
                                break;
                        }
                    }
                }

                return Questionlist;
            }
            return null;
        }

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

        public async Task<List<BOTQuestionViewModel>?> GetFlow(long BotId)
        {
            try
            {
                var questions = await _queRepo.GetQuestionOptionComponent(BotId);
                if (questions.Count != 0)
                {
                    List<BOTQuestionViewModel> models = _Mapper.Map<List<BOTQuestionViewModel>>(questions);
                    foreach (var item in models!)
                    {
                        var component = _componentRepository.GetComponentByIdAsync(item.ComponentTypeId);
                        if (component.Result!.Label == "Image" || component.Result!.Label == "File")
                        {
                            var ImageOrFile = _imageOrFileRepository.GetImageOrFileByFrontendId(item.FrontendId);
                            item.ImageOrFilePath = _imageService.GetImageBaseUrl() + (ImageOrFile.Result.ImageOrFilePath != null ? ImageOrFile.Result.ImageOrFilePath : null);
                        }

                    }
                    models = UpdateIndex(models);
                    return models;
                }
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BOTChatBotViewModel?> GetBotById(long BotId)
        {
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
                var model = _Mapper.Map<BOTChatBotViewModel>(bot);
                foreach (var item in model.Questions!)
                {
                    var component = _componentRepository.GetComponentByIdAsync(item.ComponentTypeId);
                    if (component.Result!.Label == "Image" || component.Result!.Label == "File")
                    {
                        var ImageOrFile = _imageOrFileRepository.GetImageOrFileByFrontendId(item.FrontendId);
                        item.ImageOrFilePath = _imageService.GetImageBaseUrl() + (ImageOrFile.Result.ImageOrFilePath != null ? ImageOrFile.Result.ImageOrFilePath : null);
                    }
                }
                model.Questions = UpdateIndex(model.Questions!.ToList());
                model.Avatar = _imageService.GetImageBaseUrl() + bot.Avatar;
                return model!;
            }
            return null;
        }

        public async Task<bool> editFlow(BOTChatBotModel model)
        {
            model.ApplicationUserId = _baseRepository.GetUserId();
            var bot = _Mapper.Map<BOTChatBot>(model);
            if (bot != null)
            {
                var list = await _queRepo.GetQuestionOptionComponent(bot.Id);
                if (list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        await _queRepo.DeleteQuestionList(item);
                    }
                }
                bot.Questions = updateflow(bot.Questions!.OrderBy(x => x.Sequence).ToList());
                foreach (var item in bot.Questions!)
                {
                    if (item.Id > 0) item.Id = 0;
                    if (item.Options!.Count != 0)
                    {
                        foreach (var o in item.Options!)
                        {
                            o.Id = 0;
                        }
                    }
                    if (item.Links!.Count != 0)
                    {
                        foreach (var link in item.Links!)
                        {
                            link.Id = 0;
                        }
                    }
                    
                    await _queRepo.AddQuestionAsync(item);
                }
                await _imageOrFileRepository.DeleteOtherFrontendId();
                return true;
            }
            return false;
        }

    }
}
