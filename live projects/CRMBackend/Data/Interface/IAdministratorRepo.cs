using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IAdministratorRepo
    {
        Task<ApplicationUser> AssignRole(ViewRoleModel model);

        Task AddRoleAsync(string roleModel);
    }
}
