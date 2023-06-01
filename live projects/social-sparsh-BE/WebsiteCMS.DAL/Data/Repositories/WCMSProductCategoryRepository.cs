using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Utility;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WCMSProductCategoryRepository : Repository<WCMSProductCategories>, IWCMSProductCategoryRepository
    {
        public WCMSProductCategoryRepository(WebsiteCMSDbContext context) : base(context)
        {
        }


        public async Task<List<WCMSProductCategories>?> GetAllCategoriesByUserAsync(string UserId)
        {
            try
            {
                return await Query(x => x.ApplicationUserId == UserId).Include(x => x.Products)!
                                                                      .ThenInclude(x => x.Fields.Where(x => x.IsBannerField == true))
                                                                      .ThenInclude(x => x.ProductFields)
                                                                      .ThenInclude(x => x.FieldType)
                                                                      .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<WCMSProductCategories?> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await Query(x => x.Id == id).Include(x => x.Products)!
                                                   .ThenInclude(x => x.Fields)
                                                   .ThenInclude(x => x.ProductFields)
                                                   .ThenInclude(x => x.FieldType)
                                                   .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<WCMSProductCategories> AddCategoryAsync(WCMSProductCategories category)
        {
            try
            {
                return await InsertSaveAsync(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCategoryByIdAsync(int id)
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

        /// <inheritdoc/>
        public async Task<WCMSProductCategories> UpdateCategoryAsync(WCMSProductCategories model)
        {
            try
            {
                if (model != null)
                {
                    Update(model);
                    await SaveChangesAsync();
                }

                return model!;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
