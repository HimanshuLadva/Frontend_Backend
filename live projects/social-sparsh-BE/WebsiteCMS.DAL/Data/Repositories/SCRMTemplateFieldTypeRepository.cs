using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTemplateFieldTypeRepository : SCRMITemplateFieldTypeRepository
    {
        private readonly WebsiteCMSDbContext _context;

        public SCRMTemplateFieldTypeRepository(WebsiteCMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<SCRMTemplateFieldTypeModel>> GetAllTemplateFieldTypeAsync()
        {
            var records = new List<SCRMTemplateFieldTypeModel>();
            records = await _context.tblSCRMTemplateFieldType.Select(x => new SCRMTemplateFieldTypeModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsActive = x.IsActive
                    }).OrderBy(x => x.Id).ToListAsync();
            return records;
        }

        public async Task<SCRMTemplateFieldTypeModel> GetTemplateFieldTypeByIdAsync(int id)
        {
            var record = await _context.tblSCRMTemplateFieldType.Where(x => x.Id == id)
                .Select(x => new SCRMTemplateFieldTypeModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
            return record!;
        }

        public async Task<SCRMTemplateFieldTypeModel> AddTemplateFieldTypeAsync(SCRMTemplateFieldTypeModel model)
        {
            var data = new SCRMTemplateFieldType()
            {
                Name = model.Name
            };

            _context.tblSCRMTemplateFieldType.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetTemplateFieldTypeByIdAsync(data.Id);
            return record;
        }

        public async Task<SCRMTemplateFieldTypeModel> UpdateTemplateFieldTypeAsync(int id, SCRMTemplateFieldTypeModel model)
        {
            var data = await _context.tblSCRMTemplateFieldType.FindAsync(id);
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                _context.tblSCRMTemplateFieldType.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetTemplateFieldTypeByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateTemplateFieldTypeStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMTemplateFieldType.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMTemplateFieldType.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteTemplateFieldTypeAsync(int id)
        {
            var record = new SCRMTemplateFieldType()
            {
                Id = id
            };

            _context.tblSCRMTemplateFieldType.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
