using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMUserMetaDataModel"/>.
    /// </summary>
    public interface SCRMIUserMetaDataRepository
    {
        /// <summary>
        /// Gets the all user meta data async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMUserMetaDataModel"/>.</returns>
        Task<IPagedList<SCRMUserMetaDataModel>> GetAllUserMetaDataAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the user meta data by id async.
        ///  <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMUserMetaDataModel"/>.</returns>
        Task<List<SCRMUserMetaDataModel>> GetUserMetaDataByIdAsync(string userId);

        /// <summary>
        /// Adds the user meta data async.
        ///  <para>
        ///     Gets the model of type <see cref="IFormCollection"/>.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMUserMetaData"/>.</returns>
        Task<SCRMUserMetaData> AddUserMetaDataAsync(IFormCollection model, string userId);

        /// <summary>
        /// Updates the user meta data async.
        ///  <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="IFormCollection"/>.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMUserMetaDataModel"/>.</returns>
        Task<List<SCRMUserMetaDataModel>> UpdateUserMetaDataAsync(string userId, IFormCollection model);

        /// <summary>
        /// Adds the update user meta data async.
        ///   <para>
        ///     Gets the model of type <see cref="IFormCollection"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMUserMetaDataModel"/>.</returns>
        Task<List<SCRMUserMetaDataModel>> AddUpdateUserMetaDataAsync(IFormCollection model);
    }
}
