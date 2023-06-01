using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IEventRepo
    {
        Task<EventModel> AddEventAsync(EventModel model);
        Task<bool> DeleteEventAsync(int id);
        Task<List<EventModel>> GetAllEventAsync();
        Task<EventModel> GetEventByIdAsync(int id);
        Task<EventModel> UpdateEventAsync(int id, EventModel model);
    }
}
