using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IUserSMSRepo
    {
        Task<UserSMSModel> AddSMSAsync(UserSMSModel model);
        Task<bool> DeleteSMSAsync(int id);
        Task<List<UserSMSModel>> GetAllSMSAsync();
        Task<UserSMSModel> GetSMSByIdAsync(int id);
    }
}
