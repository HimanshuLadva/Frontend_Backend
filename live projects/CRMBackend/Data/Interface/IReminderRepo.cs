using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IReminderRepo
    {
        Task<ReminderModel> AddReminderAsync(ReminderModel model);
        Task<bool> DeleteReminderAsync(int id);
        Task<List<ReminderModel>> GetAllReminderAsync();
        Task<ReminderModel> GetReminderByIdAsync(int id);
        Task<ReminderModel> UpdateReminderAsync(int id, ReminderModel model);
    }
}
