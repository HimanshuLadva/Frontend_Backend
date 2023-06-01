using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Utility;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WCMSUserTemplatesRepository : Repository<WCMSUserTemplates>, IWCMSUserTemplatesRepository
    {
        public WCMSUserTemplatesRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<List<WCMSUserTemplates>?> GetUserTemplatesAsync(string UserId)
        {
            try
            {
                return await Query(x => x.ApplicationUserId == UserId).Include(x => x.Template)
                                                            .ThenInclude(x => x!.TemplatePages)!
                                                            .ThenInclude(x => x.TemplatePageFields)!
                                                            .ThenInclude(x => x.FieldsMaster)
                                                            .ThenInclude(x => x!.FieldType)
                                                            .Include(x => x.UserTemplateDetails)!
                                                            .ThenInclude(x => x.UserTemplateDetailsChilds)
                                                            .ToListAsync();
            }
            catch (Exception) { throw; }
        }
        public async Task<bool> DeleteUserTemplateInfoByIdAsync(int id)
        {
            try
            {
                DeleteById(id);
                return await SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<WCMSUserTemplates?> AddUserTemplateInfoAsync(WCMSUserTemplates userTemplateInfo)
        {
            try
            {
                return await InsertSaveAsync(userTemplateInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<WCMSUserTemplates?> GetUserTemplateinfoByTemplateAsync(int TemplateId, string UserId)
        {
            try
            {
                return await Query(x => x.TemplateId == TemplateId && x.ApplicationUserId == UserId).Include(x => x.Template)
                                                            .ThenInclude(x => x!.TemplatePages)!
                                                            .ThenInclude(x => x.TemplatePageFields)!
                                                            .ThenInclude(x => x.FieldsMaster)
                                                            .ThenInclude(x => x!.FieldType)
                                                            .Include(x => x.UserTemplateDetails)!
                                                            .ThenInclude(x => x.UserTemplateDetailsChilds)
                                                            .FirstOrDefaultAsync();
            }
            catch (Exception) { throw; }
        }
    }
}
