using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMCaptionsModel"/>.
    /// </summary>
    public interface ISCRMCaptionService
    {
        /// <summary>
        ///     An asynchronous Method to Add Captions value in Database.
        /// <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMCaptionsModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">Gets A <see cref="{T}"/> of objects of Type <see cref="SCRMCaptionsModel"/>.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCaptionsModel"/>.</returns>
        Task<SCRMCaptionsModel> AddCaptionAsync(SCRMCaptionsModel model);

        /// <summary>
        /// Deletes the caption async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Caption record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        ///  <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<long> DeleteCaptionAsync(int captionId);

        /// <summary>
        /// Updates the caption async.
        ///  <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMCaptionsModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCaptionsModel"/>.</returns>
        Task<SCRMCaptionsModel> EditCaptionAsync(SCRMCaptionsModel model);

        /// <summary>
        /// Gets the all caption async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCaptionsModel"/>.</returns>
        Task<List<SCRMCaptionsModel>?> GetAllCaptionAsync();

        /// <summary>
        /// Gets the caption by category id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCaptionsModel"/>.</returns>
        Task<List<SCRMCaptionsModel>?> GetCaptionByCategoryId(int id);

        /// <summary>
        /// Gets the caption by sub category id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Sub Cateogry record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCaptionsModel"/>.</returns>
        Task<List<SCRMCaptionsModel>?> GetCaptionBySubCategoryId(int id);

        /// <summary>
        /// Gets the caption by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Caption record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCaptionsModel"/>.</returns>
        Task<SCRMCaptionsModel?> GetCaptionById(int id);
    }
}