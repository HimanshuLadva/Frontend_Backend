using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface ISubCategoryRepo
    {
        Task<SubCategoryModel> AddSubCateogryAsync(SubCategoryModel model);
        Task<bool> DeleteSubCategoryAsync(int id);
        Task<List<SubCategoryModel>> GetAllSubCategoryAsync();
        Task<SubCategoryModel> GetSubCategoryByIdAsync(int id);
        Task<SubCategoryModel> UpdateSubCategoryAsync(int id, SubCategoryModel model);
    }
}
