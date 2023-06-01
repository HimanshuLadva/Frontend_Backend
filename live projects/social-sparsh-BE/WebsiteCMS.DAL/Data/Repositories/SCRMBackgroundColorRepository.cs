using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMBackgroundColorRepository : SCRMIBackgroundColorRepository
    {
        private WebsiteCMSDbContext _context;
        public SCRMBackgroundColorRepository(WebsiteCMSDbContext context)
        {
            _context = context;
        }

        public async Task<IPagedList<SCRMBackgroundColorModel>> GetAllColorsAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMBackgroundColorModel>();
            records = await _context.tblSCRMColor
                   .Select(x => new SCRMBackgroundColorModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       IsActive = x.IsActive
                   }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<SCRMBackgroundColorModel> GetBackgroundColorByIdAsync(int id)
        {
            var record = await _context.tblSCRMColor.Where(x => x.Id == id)
                .Select(x => new SCRMBackgroundColorModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
            return record!;
        }

        public async Task<SCRMBackgroundColorModel> AddBackgroundColorAsync(SCRMBackgroundColorModel model)
        {
            var data = new SCRMColor()
            {
                Name = model.Name
            };

            _context.tblSCRMColor.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetBackgroundColorByIdAsync(data.Id);
            return record;
        }

        public static async Task<IPagedList<SCRMBackgroundColorModel>> SortResult(List<SCRMBackgroundColorModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMBackgroundColorModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
