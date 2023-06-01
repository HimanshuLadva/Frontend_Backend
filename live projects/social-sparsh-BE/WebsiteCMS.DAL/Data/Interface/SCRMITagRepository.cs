using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTagModel"/>.
    /// </summary>
    public interface SCRMITagRepository
    {
        /// <summary>
        /// Gets the all tag async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTagModel"/>.</returns>
        Task<IPagedList<SCRMTagModel>> GetAllTagAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the all active tag async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTagModel"/>.</returns>
        Task<List<SCRMTagModel>> GetAllActiveTagAsync();

        /// <summary>
        /// Gets the tag by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Tag record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTagModel"/>.</returns>
        Task<SCRMTagModel> GetTagByIdAsync(int id);

        /// <summary>
        /// Adds the tag async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTagModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTagModel"/>.</returns>
        Task<SCRMTagModel> AddTagAsync(SCRMTagModel model);

        /// <summary>
        /// Updates the tag async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Tag record.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTagModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTagModel"/>.</returns>
        Task<SCRMTagModel> UpdateTagAsync(int id, SCRMTagModel model);

        /// <summary>
        /// Updates the tag status async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Tag record.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateTagStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Gets the all tag wise template list async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTagModel"/>.</returns>
        Task<List<SCRMTagWiseTemplateModel>> GetAllTagWiseTemplateListAsync();

        /// <summary>
        /// Deletes the tag async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Tag record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteTagAsync(int id);
    }
}
