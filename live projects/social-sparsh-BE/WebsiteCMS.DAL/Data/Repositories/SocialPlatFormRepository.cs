using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SocialPlatFormRepository : Repository<SocialPlatforms>, ISocialPlatFormRepository
    {
        public SocialPlatFormRepository(WebsiteCMSDbContext context) : base(context)
        {
        }
        public IQueryable<SocialPlatforms> GetAllSocialPlateFormAsync()
        {
            return GetAll();
        }

        public async Task<SocialPlatforms?> GetSocialPlateFormByIdAsync(int id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SocialPlatforms> AddSocialPlateFormAsync(SocialPlatforms model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<SocialPlatforms> UpdateSocialPlateFormAsync(SocialPlatforms model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model!;
        }

        public async Task<bool> DeletePlateformAsync(int id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }
    }
}
