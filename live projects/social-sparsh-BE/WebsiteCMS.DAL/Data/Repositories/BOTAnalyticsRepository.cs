using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTAnalyticsRepository : IBOTAnalyticsRepository
    {
        private readonly WebsiteCMSDbContext _Context;
        private readonly IMapper _Mapper;
        private IBaseRepository _baseRepository;
        public BOTAnalyticsRepository(WebsiteCMSDbContext context, IMapper mapper,IBaseRepository baseRepository)
        {
            _Context = context;
            _Mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<BOTVisitorViewModel> GetVisitorChatAsync(Guid visitorUid)
        {
            try
            {
                var Visitor = await _Context.tblBOTVisitor.Include(x => x.Replies)!
                                                          .ThenInclude(x => x.Question)
                                                          .ThenInclude(x => x.Options)
                                                          .Include(x => x.Replies)!
                                                          .ThenInclude(x => x.Question)
                                                          .ThenInclude(x => x.ComponentType)
                                                          .Where(x => x.VisitorUUId == visitorUid)
                                                          .FirstOrDefaultAsync();
                var model = _Mapper.Map<BOTVisitorViewModel>(Visitor!);
                return model;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }

        public async Task<Dictionary<string, object>> GetLeadsByBotAsync(long botId, int pageNumber)
        {
            try
            {
                var visitors = await _Context.tblBOTVisitor.Include(x => x.Replies)
                                                           .Where(x => x.Replies!.First().ChatBotId == botId)
                                                           .ToListAsync();
                var models = _Mapper.Map<List<BOTVisitorViewModel>>(visitors);
                var dict = new Dictionary<string, object>
                {
                    { "VisitorCount", models.Count },
                    { "Visitors", models.Skip((pageNumber - 1) * 10).Take(10).ToList() }
                };
                return dict;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }

        public async Task<Dictionary<string, object>> GetLeadsByUserAsync(int pageNumber)
        {
            try
            {
                var UserId = _baseRepository.GetUserId();
                var visitors = await _Context.tblBOTVisitor.Include(x => x.Replies)!
                                                           .ThenInclude(x => x.ChatBot)
                                                           .Where(x => x.Replies!.First().ChatBot.ApplicationUserId == UserId)
                                                           .ToListAsync();
                var models = _Mapper.Map<List<BOTVisitorViewModel>>(visitors);
                var dict = new Dictionary<string, object>
                {
                    { "VisitorCount", models.Count },
                    { "Visitors", models.Skip((pageNumber - 1) * 10).Take(10).ToList() }
                };
                return dict;
            }
            catch (Exception e)
            {
                throw new CustomDBException(e, _Context);
            }
        }

    }
}