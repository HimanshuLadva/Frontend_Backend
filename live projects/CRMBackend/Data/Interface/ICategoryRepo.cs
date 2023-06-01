using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface ICategoryRepo
    {
        Task<CategoryModel> AddCateogryAsync(CategoryModel model);
        Task<bool> DeleteCategoryAsync(int id);
        Task<List<CategoryModel>> GetAllCategoryAsync(int groupId);
        Task<CategoryModel> GetCategoryByIdAsync(int groupId, int id);
        Task<CategoryModel> UpdateCategoryAsync(int id, CategoryModel model);
    }
}
