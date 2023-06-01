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
    public class SCRMLanguageRepository : SCRMILanguageRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public SCRMLanguageRepository(WebsiteCMSDbContext context)
        {
            _context = context;
        }
        public async Task<IPagedList<SCRMLanguageModel>> GetAllLanguageAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMLanguageModel>();
            records = await _context.tblSCRMLanguage
                   .Select(x => new SCRMLanguageModel()
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
        public async Task<SCRMLanguageModel> GetLanguageByIdAsync(int id)
        {
            var record = await _context.tblSCRMLanguage.Where(x => x.Id == id)
                .Select(x => new SCRMLanguageModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
            return record!;
        }
        public async Task<SCRMLanguageModel> AddLanguageAsync(SCRMLanguageModel model)
        {
            var data = new SCRMLanguage()
            {
                Name = model.Name
            };

            _context.tblSCRMLanguage.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetLanguageByIdAsync(data.Id);
            return record;
        }
        public async Task<SCRMLanguageModel> UpdateLanguageAsync(int id, SCRMLanguageModel model)
        {
            var data = await _context.tblSCRMLanguage.FindAsync(id);
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                _context.tblSCRMLanguage.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetLanguageByIdAsync(data.Id);
                return record;
            }
            return null!;
        }
        public async Task<bool> UpdateLanguageStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMLanguage.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMLanguage.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public static async Task<IPagedList<SCRMLanguageModel>> SortResult(List<SCRMLanguageModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMLanguageModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
