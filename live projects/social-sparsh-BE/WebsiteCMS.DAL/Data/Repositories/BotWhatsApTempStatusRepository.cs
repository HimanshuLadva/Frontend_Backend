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
    public class BotWhatsApTempStatusRepository : Repository<BOTWhatsAppTemplatesStatus>, IBotWhatsApTempStatusRepository
    {
        public BotWhatsApTempStatusRepository(WebsiteCMSDbContext context) : base(context)
        {
        }
        public Task<List<BOTWhatsAppTemplatesStatus>> getAllWhatsappTempStatus()
        {
            return GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BOTWhatsAppTemplatesStatus?> getStatusById(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<BOTWhatsAppTemplatesStatus?> getStatusByTempId(string id)
        {
            return await Query(x => x.WhatsAppTemplateId == id).FirstOrDefaultAsync();
        }

        public async Task<BOTWhatsAppTemplatesStatus> AddTemplateStatusAsync(BOTWhatsAppTemplatesStatus model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTWhatsAppTemplatesStatus> UpdateTemplateStatusAsync(BOTWhatsAppTemplatesStatus model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteTemplateStatusAsync(int id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }
    }
}
