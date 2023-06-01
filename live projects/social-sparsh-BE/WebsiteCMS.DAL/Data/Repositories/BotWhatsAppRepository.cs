using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BotWhatsAppRepository : Repository<BOTWhatsAppBusinessData>, IBotWhatsAppRepository
    {
        public BotWhatsAppRepository(WebsiteCMSDbContext context) : base(context)
        {
        }
        public async Task<BOTWhatsAppBusinessData?> GetWABAByBussinessIdAsync(string id)
        {
            return await Query(x => x.BusinessId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BOTWhatsAppBusinessData>> GetAllWABData()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BOTWhatsAppBusinessData?> GetWABAByIdAsync(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<BOTWhatsAppBusinessData> AddWABAAsync(BOTWhatsAppBusinessData model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTWhatsAppBusinessData?> UpdateWABAAsync(BOTWhatsAppBusinessData model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteWABAccount(long id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }

        public async Task<BOTWhatsAppBusinessData?> getWABAByChatBotID(long id)
        {
            return await Query(x => x.ChatBotId == id).FirstOrDefaultAsync();
        }



    }
}
