using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Utility;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WCMSProductRepository : Repository<WCMSCategoryWiseProducts>, IWCMSProductRepository
    {
        public WCMSProductRepository(WebsiteCMSDbContext context) : base(context)
        {
        }


        public async Task<List<WCMSCategoryWiseProducts>> GetAllProductsByCategoryAsync(int CategoryId)
        {
            try
            {
                return await Query(x => x.ProductCategoryId == CategoryId).Include(x => x.Fields).ThenInclude(x => x.ProductFields).ThenInclude(x => x.FieldType).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<WCMSCategoryWiseProducts?> GetProductByIdAsync(int id)
        {
            try
            {
                return await Query(x => x.Id == id).Include(x => x.Fields).ThenInclude(x => x.ProductFields).ThenInclude(x => x.FieldType).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WCMSCategoryWiseProducts?> AddProductAsync(WCMSCategoryWiseProducts product)
        {
            try
            {
                return await InsertSaveAsync(product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<WCMSCategoryWiseProducts> UpdateProductAsync(WCMSCategoryWiseProducts product)
        {
            try
            {
                if (product != null)
                {
                    Update(product);
                    await SaveChangesAsync();
                }

                return product!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
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
