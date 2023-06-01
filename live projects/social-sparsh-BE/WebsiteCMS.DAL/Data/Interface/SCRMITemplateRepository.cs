using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using WebsiteCMS.DAL.Models;
using X.PagedList;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTemplateModel"/>.
    /// </summary>
    public interface SCRMITemplateRepository
    {

        /// <summary>
        /// Gets the all template async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTemplateModel"/>.</returns>
        Task<IPagedList<SCRMTemplateModel>> GetAllTemplateAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Adds the template async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTemplateModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateModel"/>.</returns>
        Task<SCRMTemplateModel> AddTemplateAsync(SCRMTemplateModel model);

        /// <summary>
        /// Gets the template by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Template record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateModel"/>.</returns>
        Task<SCRMTemplateModel> GetTemplateByIdAsync(int id);

        /// <summary>
        /// Updates the template async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Template record.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTemplateModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateModel"/>.</returns>
        Task<SCRMTemplateModel> UpdateTemplateAsync(int id, SCRMTemplateModel model);

        /// <summary>
        /// Updates the template status async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Template record.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateTemplateStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the template async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Template record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteTemplateAsync(int id);
    }
}
