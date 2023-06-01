using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMUserModel"/>.
    /// </summary>
    public interface SCRMIUserRepository
    {
        /// <summary>
        /// Gets the all user async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMUserModel"/>.</returns>
        Task<IPagedList<SCRMUserModel>> GetAllUserAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the user by id async.
        ///  <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMUserModel"/>.</returns>
        Task<SCRMUserModel> GetUserByIdAsync(string userId);

        /// <summary>
        /// Adds the user async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMUserModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMUserModel"/>.</returns>
        Task<SCRMUserModel> AddUserAsync(SCRMUserModel model);

        /// <summary>
        /// Updates the user async.
        ///   <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMUserModel"/>.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMUserModel"/>.</returns>
        Task<SCRMUserModel> UpdateUserAsync(string userId, SCRMUserModel model);

        /// <summary>
        /// Deletes the user async.
        ///   <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A Task.</returns>
        Task DeleteUserAsync(string userId);
    }
}
