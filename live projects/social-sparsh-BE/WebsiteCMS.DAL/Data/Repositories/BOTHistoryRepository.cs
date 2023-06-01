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
    public class BOTHistoryRepository : Repository<BOTHistory>, IBOTHistoryRepository
    {
        public BOTHistoryRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTHistory> AddReplyAsync(BOTHistory model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTHistory?> GetHistory(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<BOTHistory?> GetHistoryByVisitorId(long id)
        {
            return await Query(x => x.VisitorId == id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteLastQuesAsync(long id)
        {
            DeleteById(id);
            return await SaveChangesAsync() > 0;
        }
    }
}
