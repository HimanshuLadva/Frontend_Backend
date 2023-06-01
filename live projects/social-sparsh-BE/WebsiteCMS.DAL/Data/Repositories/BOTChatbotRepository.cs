using Microsoft.EntityFrameworkCore;
using System;
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
    public class BOTChatBOTRepository : Repository<BOTChatBot>, IBOTChatBOTRepository
    {
        public BOTChatBOTRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<List<BOTChatBot>> GetAllBotAsync()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BOTChatBot?> GetBotByIdAsync(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<BOTChatBot> AddBotAsync(BOTChatBot model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTChatBot?> UpdateBotAsync(BOTChatBot model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteBotAsync(long id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }

        public async Task<BOTChatBot?> GetBotByUserIdAsync(string id)
        {
            return await Query(x => x.ApplicationUserId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BOTChatBot>> GetBotListByUserIdAsync(string id)
        {
            return await Query(x => x.ApplicationUserId == id).ToListAsync();
        }

        public async Task<BOTChatBot?> GetBotFlow(long id)
        {
            return await Query(x => x.Id == id).IncludeEntities(x => x.Questions!).IncludeEntities(x => x.Platforms!).FirstOrDefaultAsync();
        }       

        public async Task<List<BOTChatBot>> GetExceptQuestionBot(BOTChatBot model)
        {
            return await Query(x => x.Id == model.Id && x.Questions != model.Questions).ToListAsync();
        }
    }
}
