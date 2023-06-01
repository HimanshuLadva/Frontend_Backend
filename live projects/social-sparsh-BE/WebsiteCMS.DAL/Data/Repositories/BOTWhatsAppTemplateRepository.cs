using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTWhatsAppTemplateRepository : Repository<BOTWhatsAppTemplates>, IBOTWhatsAppTemplateRepository
    {
        public BOTWhatsAppTemplateRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByTempID(string id)
        {
            return await Query(x => x.WhatsAppTemplateId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BOTWhatsAppTemplates>> GetAllWhatsAppTemplates()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByIdAsync(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<BOTWhatsAppTemplates> AddWhatsappTemplate(BOTWhatsAppTemplates model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTWhatsAppTemplates?> UpdateWhatsAppTemplate(BOTWhatsAppTemplates model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteWhatsAppTemplate(long id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }

        public async Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByQueID(long id)
        {
            return await Query(x => x.QuestionId == id).FirstOrDefaultAsync();
        }
    }
}
