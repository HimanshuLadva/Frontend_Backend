using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class SubCategoryRepo : Repository<SubCategories>, ISubCategoryRepo
    {
        private readonly RMbackendContext _context;

        public SubCategoryRepo(RMbackendContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SubCategoryModel> AddSubCateogryAsync(SubCategoryModel model)
        {
            var data = new SubCategories()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
            };
            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetSubCategoryByIdAsync(data.Id);
            return result;
        }

        public async Task<List<SubCategoryModel>> GetAllSubCategoryAsync()
        {
            var data = await GetAll().Select(x => new SubCategoryModel()
            {
                Id = x.Id,
                Name = x.Name!,
                CategoryId = x.CategoryId
            }).ToListAsync();

            return data!;
        }

        public async Task<SubCategoryModel> GetSubCategoryByIdAsync(int id)
        {
            var data = await Query(x => x.Id == id).Select(x => new SubCategoryModel()
            {
                Id = x.Id,
                Name = x.Name!,
                CategoryId = x.CategoryId
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<SubCategoryModel> UpdateSubCategoryAsync(int id, SubCategoryModel model)
        {
            var data = await Query(x => x.Id == id).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = id;
                data.Name = model.Name;
                data.CategoryId = data.CategoryId;
            }

            Update(data!);
            await SaveChangesAsync();

            var result = await GetSubCategoryByIdAsync(data.Id);
            return result;
        }

        public async Task<bool> DeleteSubCategoryAsync(int id)
        {
            var data = new SubCategories()
            {
                Id = id
            };

            Delete(data);
            await SaveChangesAsync();

            return true;
        }
    }
}
