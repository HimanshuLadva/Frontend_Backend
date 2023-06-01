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
    public class BOTWhatsAppTempRegisterIssus : Repository<BOTWhatsAppTemplateRegisterIssue>, IBOTWhatsAppTempRegisterIssus
    {
        public BOTWhatsAppTempRegisterIssus(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTWhatsAppTemplateRegisterIssue> AddIssue(BOTWhatsAppTemplateRegisterIssue model)
        {
            await InsertSaveAsync(model);
            return model;
        }

    }
}
