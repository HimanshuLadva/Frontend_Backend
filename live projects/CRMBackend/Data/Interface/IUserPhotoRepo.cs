using CRMBackend.Data.Models;

namespace CRMBackend.Data.Interface
{
    public interface IUserPhotoRepo
    {
        Task<UserPhotoViewModel> AddPhotoAsync(int contactId, UserPhotoModel model);
        Task<bool> DeletePhotoAsync(int contactId, int id);
        Task<List<UserPhotoViewModel>> GetAllPhotoAsync(int contactId);
        Task<UserPhotoViewModel> GetPhotoByIdAsync(int id, int contactId);
        Task<UserPhotoViewModel> UpdatePhotoAsync(int contactId, int id, UserPhotoModel model);
    }
}
