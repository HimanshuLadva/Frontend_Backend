using CRMBackend.Data.Models;

namespace CRMBackend.Data.Interface
{
    public interface IUserNoteRepo
    {
        Task<UserNoteModel> AddNoteAsync(int contactId, UserNoteModel model);
        Task<bool> DeleteNoteAsync(int contactId, int id);
        Task<List<UserNoteModel>> GetAllNoteAsync(int contactId);
        Task<UserNoteModel> GetNoteByIdAsync(int id, int contactId);
        Task<UserNoteModel> UpdateNoteAsync(int contactId, int id, UserNoteModel model);
    }
}
