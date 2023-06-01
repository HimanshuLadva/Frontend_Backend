
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
    public class BOTVisitorRepository : Repository<BOTVisitor>, IBOTVisitorRepository
    {
        public BOTVisitorRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTVisitor?> GetBotVisitorByUUID(Guid id)
        {
            return await Query(x => x.VisitorUUId == id).FirstOrDefaultAsync();
        }

        public async Task<BOTVisitor?> GetBotVisitorById(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<BOTVisitor>> GetAllVisitor()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BOTVisitor> AddVisitorAsync(BOTVisitor model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTVisitor?> UpdateVisitorAsync(BOTVisitor model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model;
        }
    }
}
