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
    public class SCRMFontFamilyRepository : SCRMIFontFamilyRepository
    {
        private readonly WebsiteCMSDbContext _context;

        public SCRMFontFamilyRepository(WebsiteCMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<SCRMFontFamilyModel>> GetAllFontFamilyAsync()
        {
            var records = new List<SCRMFontFamilyModel>();
            records = await _context.tblSCRMFontFamily
                   .Select(x => new SCRMFontFamilyModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       IsActive = x.IsActive
                   }).OrderBy(x => x.Name).ToListAsync();
            return records;
        }

        public async Task<SCRMFontFamilyModel> GetFontFamilyByIdAsync(int id)
        {
            var record = await _context.tblSCRMFontFamily.Where(x => x.Id == id)
                .Select(x => new SCRMFontFamilyModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
            return record!;
        }

        public async Task<SCRMFontFamilyModel> AddFontFamilyAsync(SCRMFontFamilyModel model)
        {
            var data = new SCRMFontFamily()
            {
                Name = model.Name
            };

            _context.tblSCRMFontFamily.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetFontFamilyByIdAsync(data.Id);
            return record;
        }

        public async Task<SCRMFontFamilyModel> UpdateFontFamilyAsync(int id, SCRMFontFamilyModel model)
        {
            var data = await _context.tblSCRMFontFamily.FindAsync(id);
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                _context.tblSCRMFontFamily.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetFontFamilyByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateFontFamilyStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMFontFamily.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMFontFamily.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteFontFamilyAsync(int id)
        {
            var record = new SCRMFontFamily()
            {
                Id = id
            };

            _context.tblSCRMFontFamily.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
