using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Utility;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WCMSProductFieldsRepository : Repository<WCMSProductFields>, IWCMSProductFieldsRepository
    {
        public WCMSProductFieldsRepository(WebsiteCMSDbContext context) : base(context)
        {
        }


        public async Task<List<WCMSProductFields>> GetAllProductFieldsAsync()
        {
            try
            {
                return await GetAll().IncludeEntities(x => x.FieldType).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddProductFieldsAsync(List<WCMSProductFields> Fields)
        {
            try
            {
                InsertRange(Fields);
                return await SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WCMSProductFields?> UpdateProductFieldAsync(WCMSProductFields Field)
        {
            try
            {
                if (Field != null)
                {
                    Update(Field);
                    await SaveChangesAsync();
                }
                return Field!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProductFieldAsync(int id)
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
    }
}
