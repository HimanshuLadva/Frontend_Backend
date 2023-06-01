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
    public class SCRMCaptionRepository : Repository<SCRMCaptions>, ISCRMCaptionRepository
    {
        public SCRMCaptionRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<List<SCRMCaptions>> GetAllCaptionAsync()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<SCRMCaptions?> GetCaptionByIdAsync(int id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<SCRMCaptions>?> GetCaptionByCategoryIdAsync(int id)
        {
            return await Query(x => x.SCRMCategoryID == id).ToListAsync();
        }

        public async Task<List<SCRMCaptions>?> GetCaptionBySubCategoryIdAsync(int id)
        {
            return await Query(x => x.SCRMSubCategoryId == id).ToListAsync();
        }

        public async Task<SCRMCaptions> AddCaptionAsync(SCRMCaptions model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<SCRMCaptions> UpdateCaptionAsync(SCRMCaptions model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteCaptionAsync(int id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }
    }
}
