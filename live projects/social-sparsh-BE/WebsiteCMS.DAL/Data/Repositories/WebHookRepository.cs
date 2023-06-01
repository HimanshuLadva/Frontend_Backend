using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HPSF;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class WebHookRepository : Repository<BOTWebHookResponse>, IWebHookRepository
    {
        public WebHookRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTWebHookResponse> AddResponseAsync(BOTWebHookResponse model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTWebHookResponse?> GetResponseByFromAsync(string FromNo)
        {
            return await Query(x => x.From == FromNo).FirstOrDefaultAsync();
        }

    }
}
