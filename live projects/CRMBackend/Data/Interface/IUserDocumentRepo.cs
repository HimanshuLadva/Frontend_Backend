using CRMBackend.Data.Models;

namespace CRMBackend.Data.Interface
{
    public interface IUserDocumentRepo
    {
        Task<UserDocumentViewModel> AddDocumentAsync(int contactId, UserDocumentModel model);
        Task<bool> DeleteDocumentAsync(int contactId, int id);
        Task<List<UserDocumentViewModel>> GetAllDocumentAsync(int contactId);
        Task<UserDocumentViewModel> GetDocumentByIdAsync(int id, int contactId);
        Task<UserDocumentViewModel> UpdateDocumentAsync(int contactId, int id, UserDocumentModel model);
    }
}
