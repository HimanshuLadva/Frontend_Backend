using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IUserEmailRepo
    {
        Task<UserEmailModel> AddEmailAsync(UserEmailModel model);
        Task<bool> DeleteEmailAsync(int id);
        Task<List<UserEmailModel>> GetAllEmailAsync();
        Task<UserEmailModel> GetEmailByIdAsync(int id);
    }
}
