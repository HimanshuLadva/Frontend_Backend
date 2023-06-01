using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Crypt.Dsig;
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
    public class BusinessCategoryRepository : Repository<BusinessCategory>, IBusinessCategoryRepository
    {
        private readonly WebsiteCMSDbContext _context;

        public BusinessCategoryRepository(WebsiteCMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BusinessCategoryModel> GetCategoryByIdAsync(int id)
        {
            var record = await Query(x => x.Id == id).Select(x => new BusinessCategoryModel()
            {
                Id = x.Id,
                Name = x!.Name
            }).FirstOrDefaultAsync();
            return record!;
        }
        public async Task<BusinessCategoryModel> AddCategory(BusinessCategoryModel model)
        {
            var data = new BusinessCategory()
            {
                Name = model.Name
            };
            await InsertAsync(data);
            await SaveChangesAsync();
            var record = await GetCategoryByIdAsync(data.Id);
            return record;
        }

        public async Task<IPagedList<BusinessCategoryModel>> GetAllCategoryAsync(SCRMRequestParams requestParams)
        {
            var records = new List<BusinessCategoryModel>();

            records = await GetAll().Select(x => new BusinessCategoryModel()
            {
                Id = x.Id,
                Name = x!.Name
            }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.Contains(requestParams.search)).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<BusinessCategoryModel> UpdateCategoryAsync(int id, string Name)
        {
            var data = await GetByIDAsync(id);

            if (data != null)
            {
                data.Id = id;
                data.Name = Name;

                //_context.tblBusinessCategorys.Update(data);
                Update(data);
                await SaveChangesAsync();

                var record = await GetCategoryByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public static async Task<IPagedList<BusinessCategoryModel>> SortResult(List<BusinessCategoryModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<BusinessCategoryModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var record = new BusinessCategory()
            {
                Id = id
            };

            var categoryExistsInBusinessInfo = _context.tblBusinessInfoCategorys.Where(x => x.BusinessCategoryId == id).FirstOrDefault();
            if (categoryExistsInBusinessInfo == null)
            {
                Delete(record);
                await SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
