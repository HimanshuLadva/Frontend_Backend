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
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMLanguageModel"/>.
    /// </summary>
    public interface SCRMILanguageRepository
    {
        /// <summary>
        /// Gets the all language async.
        /// <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMLanguageModel"/>.</returns>
        Task<IPagedList<SCRMLanguageModel>> GetAllLanguageAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the language by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Language record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMLanguageModel"/>.</returns>
        Task<SCRMLanguageModel> GetLanguageByIdAsync(int id);

        /// <summary>
        /// Adds the language async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMLanguageModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMLanguageModel"/>.</returns>
        Task<SCRMLanguageModel> AddLanguageAsync(SCRMLanguageModel model);

        /// <summary>
        /// Updates the language async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Language record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMLanguageModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMLanguageModel"/>.</returns>
        Task<SCRMLanguageModel> UpdateLanguageAsync(int id, SCRMLanguageModel model);

        /// <summary>
        /// Updates the language status async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Language record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateLanguageStatusAsync(int id, SCRMUpdateStatusModel model);
    }
}
