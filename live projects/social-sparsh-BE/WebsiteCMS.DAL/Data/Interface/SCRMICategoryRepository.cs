using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMCategoryModel"/>.
    /// </summary>
    public interface SCRMICategoryRepository
    {
        /// <summary>
        /// Gets the all category async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMCategoryModel"/>.</returns>
        Task<IPagedList<SCRMCategoryModel>> GetAllCategoryAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the all active category async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCategoryModel"/>.</returns>
        Task<List<SCRMCategoryModel>> GetAllActiveCategoryAsync();

        /// <summary>
        /// Gets the category by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCategoryModel"/>.</returns>
        Task<SCRMCategoryModel> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Adds the category async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMCategoryModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCategoryModel"/>.</returns>
        Task<SCRMCategoryModel> AddCategoryAsync(SCRMCategoryModel model);

        /// <summary>
        /// Updates the category async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMCategoryModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCategoryModel"/>.</returns>
        Task<SCRMCategoryModel> UpdateCategoryAsync(int id, SCRMCategoryModel model);

        /// <summary>
        /// Updates the category status async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateCategoryStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the category async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteCategoryAsync(int id);

        /// <summary>
        /// Gets the all category wise template list async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMCategoryModel"/>.</returns>
        Task<IPagedList<SCRMCategroyWiseTemplateModel>> GetAllCategoryWiseTemplateListAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the category wise template list async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCategroyWiseTemplateModel"/>.</returns>
        Task<List<SCRMCategroyWiseTemplateModel>> GetCategoryWiseTemplateListAsync(int id);

        /// <summary>
        /// Gets the all multiple category wise template list async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMMultipleCategorys"/>.
        /// </para>
        /// </summary>
        /// <param name="categorys">The categorys.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCategoryModel"/>.</returns>
        Task<List<SCRMMultipleCategoryWiseTemplateModel>> GetAllMultipleCategoryWiseTemplateListAsync(SCRMMultipleCategorys categorys);
    }
}
