using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Utility;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BotQuestionRepository : Repository<BOTQuestion>, IBotQuestionRepository
    {
        private readonly IBOTComponentRepository _componentRepository;
        private readonly IBOTImageOrFileRepository _imageOrFileRepository;

        public BotQuestionRepository(WebsiteCMSDbContext context, IBOTComponentRepository componentRepository, IBOTImageOrFileRepository imageOrFileRepository) : base(context)
        {
            _componentRepository = componentRepository;
            _imageOrFileRepository = imageOrFileRepository;
        }

        public async Task<List<BOTQuestion>?> GetAllQuestionByBotID(long id)
        {
            return await Query(x => x.ChatBotId == id && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<BOTQuestion>?> GetAllQuestion()
        {
            return await Query(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<BOTQuestion>> GetQuestionOptionComponent(long id)
        {
            return await Query(x => x.ChatBotId == id && x.IsDeleted == false).IncludeEntities(x => x.Options!).IncludeEntities(x => x.ComponentType).IncludeEntities(x => x.Links!).ToListAsync();
        }

        public async Task<BOTQuestion?> GetQuestionComponentById(long id)
        {
            return await Query(x => x.Id == id && x.IsDeleted == false).IncludeEntities(x => x.ComponentType).FirstOrDefaultAsync();
        }

        public async Task<List<BOTQuestion>> GetExceptQuestion(List<BOTQuestion> model, long id)
        {
            return await Query(x => x.ChatBotId == id && x.IsDeleted == false).Except(model).IncludeEntities(x => x.Options!).ToListAsync();
        }

        public async Task<bool> DeleteQuestionList(BOTQuestion model)
        {
            Delete(model);
            return await SaveChangesAsync() > 0;
        }

        public async Task<BOTQuestion> AddQuestionAsync(BOTQuestion model)
        {
            var ComponentLabel = await _componentRepository.GetComponentByIdAsync(model.ComponentTypeId);
            //var imageOrFiletbl = await Query(x => x.FrontendId == model.FrontendId).FirstOrDefaultAsync();
            var imageOrFiletbl = await _imageOrFileRepository.GetImageOrFileByFrontendId(model.FrontendId);

            if (ComponentLabel!.Label == "Image" || ComponentLabel!.Label == "File")
            {
                BOTImageOrFile ImageOrFile;
                if (imageOrFiletbl != null)
                {
                    ImageOrFile = new BOTImageOrFile()
                    {
                        Id = imageOrFiletbl.Id,
                        ImageOrFilePath = imageOrFiletbl.ImageOrFilePath,
                        FrontendId = model.FrontendId,
                    };
                }
                else
                {
                    ImageOrFile = new BOTImageOrFile()
                    {
                        ImageOrFilePath = ComponentLabel!.Label == "Image" ? "BOTDefault/weybee_logo.png" : "BOTDefault/weybee_logo.pdf",
                        FrontendId = model.FrontendId,
                    };
                }

                await _imageOrFileRepository.AddImageorFile(ImageOrFile);
            }

            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTQuestion?> GetQuestionByTargetId(string targetId, long botId)
        {
            return await Query(x => x.FrontendId == targetId && x.ChatBotId == botId && x.IsDeleted == false).IncludeEntities(x => x.Options!).IncludeEntities(x => x.ComponentType).FirstOrDefaultAsync();
        }

        public IQueryable<BOTQuestion?> GetQuestionByFrontendId(string frontendId)
        {
            return Query(x => x.FrontendId == frontendId && x.IsDeleted == false);
        }

        public async Task<BOTQuestion> UpdateQuestions(BOTQuestion model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

    }
}
