using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTemplateFieldModel"/>.
    /// </summary>
    public interface SCRMITemplateFieldRepository
    {
        /// <summary>
        /// Gets the all template field async.
        ///    <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="string"/> baseUrl for image.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <param name="baseURL">The base u r l.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTemplateFieldModel"/>.</returns>
        Task<IPagedList<SCRMTemplateFieldModel>> GetAllTemplateFieldAsync(SCRMRequestParams requestParams, string baseURL);

        /// <summary>
        /// Gets the template field by id async.
        ///    <para>
        ///     Gets The <see cref="int"/> Id of template field record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldModel"/>.</returns>
        Task<SCRMTemplateFieldModel> GetTemplateFieldByIdAsync(int id);

        /// <summary>
        /// Adds the template field async.
        ///     <para>
        ///     Gets the model of type <see cref="SCRMTemplateFieldModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldModel"/>.</returns>
        Task<SCRMTemplateFieldModel> AddTemplateFieldAsync(SCRMTemplateFieldModel model);

        /// <summary>
        /// Updates the template field async.
        ///     <para>
        ///     Gets The <see cref="int"/> Id of template field record.
        /// </para>
        ///      <para>
        ///     Gets the model of type <see cref="SCRMTemplateFieldModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldModel"/>.</returns>
        Task<SCRMTemplateFieldModel> UpdateTemplateFieldAsync(int id, SCRMTemplateFieldModel model);

        /// <summary>
        /// Updates the template field status async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of template field record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateTemplateFieldStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the template field async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of template field record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteTemplateFieldAsync(int id);
    }
}
