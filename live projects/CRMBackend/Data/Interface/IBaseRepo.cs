using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IBaseRepo
    {
        Task<ApplicationUser> GetCurrentUserAsync();
    }
}
