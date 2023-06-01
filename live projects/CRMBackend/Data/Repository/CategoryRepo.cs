using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class CategoryRepo : Repository<Categories>, ICategoryRepo
    {
        private readonly RMbackendContext _context;

        public CategoryRepo(RMbackendContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CategoryModel> AddCateogryAsync(CategoryModel model)
        {
            var data = new Categories()
            {
                Name = model.Name,
                GroupId = model.GroupId
            };
            await InsertAsync(data);
            await SaveChangesAsync();
            var result = await GetCategoryByIdAsync(data.GroupId, data.Id);
            return result;
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync(int groupId)
        {
            var data = await GetAll().Where(x => x.GroupId == groupId).Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name!,
                GroupId = x.GroupId
            }).ToListAsync();

            return data;
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int groupId, int id)
        {
            var data = await Query(x => x.Id == id && x.GroupId == groupId).Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name!,
                GroupId = x.GroupId
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<CategoryModel> UpdateCategoryAsync(int id, CategoryModel model)
        {
            var data = await Query(x => x.Id == id).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = id;
                data.Name = model.Name;
                data.GroupId = data.GroupId;
            }

            Update(data!);
            await SaveChangesAsync();
            var result = await GetCategoryByIdAsync(data!.GroupId, data.Id);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var data = new Categories()
            {
                Id = id
            };

            Delete(data);
            await SaveChangesAsync();

            return true;
        }
    }
}
