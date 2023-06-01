using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class BOTWebAppChannelService : IBOTWebAppChannelService
    {
        private readonly IBOTchatbotService _chatbot;
        private readonly IBotQuestionRepository _question;
        private readonly IBOTHistoryRepository _history;
        private readonly IMapper _Mapper;
        public readonly IBOTVisitorRepository _visitor;

        public BOTWebAppChannelService(IBOTchatbotService chatbot, IMapper mapper, IBOTVisitorRepository visitor, IBotQuestionRepository question, IBOTHistoryRepository history)
        {
            _chatbot = chatbot;
            _Mapper = mapper;
            _visitor = visitor;
            _history = history;
            _question = question;
        }
        public async Task<object?> Execute(BOTHistoryModel model)
        {
            //var visitor = await _Context.tblBOTVisitor.Where(x => x.VisitorUUId == model.VisitorUUId).FirstOrDefaultAsync();
            var visitor = await _visitor.GetBotVisitorByUUID(model.VisitorUUId);
            var platform = visitor!.Platform == string.Empty ? "web" : visitor.Platform;
            visitor!.Platform = platform;
            await _visitor.UpdateVisitorAsync(visitor);
            var record = await _chatbot.SaveReplyAsync(model);
            if (record != null)
            {
                var result = await GenerateResponse(record);
                return result;
            }
            return null;
        }

        public async Task<object> GenerateResponse(BOTHistoryModel model)
        {
            var reply = await _history.GetHistory((long)model.Id);
            var next = await _question.GetQuestionByTargetId(reply!.Question.Target, reply.ChatBotId);
            return _Mapper.Map<BOTQuestionViewModel>(next);
        }
    }
}
