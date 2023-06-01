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
    public class WCMSTemplatesRepository : Repository<WCMSTemplates>, IWCMSTemplatesRepository
    {
        public WCMSTemplatesRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public List<WCMSTemplates> GetAllTemplatesAsync()
        {
            return GetAll().OrderBy(x => x.Id).ToList();
        }

    }
}
