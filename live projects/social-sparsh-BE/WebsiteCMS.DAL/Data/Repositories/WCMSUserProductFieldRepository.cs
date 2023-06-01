using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WCMSUserProductFieldRepository : Repository<WCMSUserProductFields>, IWCMSUserProductFieldRepository
    {
        public WCMSUserProductFieldRepository(WebsiteCMSDbContext context) : base(context)
        {
        }


        public async Task<bool> DeleteUserProductFieldAsync(int id)
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

        public async Task<List<WCMSUserProductFields>> GetAllUserFieldsAsync(int productId)
        {
            try
            {
                return await Query(x => x.ProductsId == productId, true).Include(x => x.ProductFields).ThenInclude(x => x.FieldType).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddUserProductFieldsAsync(List<WCMSUserProductFields> Fields)
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
    }
}
