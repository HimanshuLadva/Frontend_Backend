using Microsoft.AspNetCore.Identity;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="ApplicationUser"/>.
    /// </summary>
    public interface IAdministratorRepository
    {
        /// <summary>
        /// Assigns the role.
        ///    <para>
        ///     Gets the model of type <see cref="ViewRoleModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="ApplicationUser"/>.</returns>
        Task<ApplicationUser> AssignRole(ViewRoleModel model);

        /// <summary>
        /// Adds the role async.
        ///  <para>
        ///     Gets The <see cref="string"/> roleModel is name of role.
        /// </para>
        /// </summary>
        /// <param name="roleModel">The role model.</param>
        /// <returns>A Task.</returns>
        Task AddRoleAsync(string roleModel);
    }
}
