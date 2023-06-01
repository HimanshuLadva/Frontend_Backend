using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMAlignRepository : Repository<SCRMAlign>, ISCRMAlignRepository
    {
        public SCRMAlignRepository(WebsiteCMSDbContext context) : base(context)
        {
        }
        public Task<List<SCRMAlign>> GetAllAlignAsync()
        {
            return GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<SCRMAlign?> GetAlignByIdAsync(int id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SCRMAlign> AddAlignAsync(SCRMAlign model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<SCRMAlign> UpdateAlignAsync(SCRMAlign model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }

        public async Task<bool> DeleteAlignAsync(int id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }
    }
}