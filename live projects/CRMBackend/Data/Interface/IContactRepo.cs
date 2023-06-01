using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IContactRepo
    {
        Task<ContactModel> AddContactAsync(ContactModel model);
        Task<bool> DeleteContactAsync(int id);
        Task<List<ContactModel>> GetAllContactAsync();
        Task<ContactModel> GetContactByIdAsync(int id);
        Task<ContactModel> UpdateContactAsync(int id, ContactModel model);
    }
}
