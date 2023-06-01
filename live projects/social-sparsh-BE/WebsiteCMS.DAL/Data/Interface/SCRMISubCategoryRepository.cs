using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMSubCategoryModel"/>.
    /// </summary>
    public interface SCRMISubCategoryRepository
    {
        /// <summary>
        /// Gets the all sub category async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMSubCategoryModel"/>.</returns>
        Task<IPagedList<SCRMSubCategoryModel>> GetAllSubCategoryAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the all active sub category async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMSubCategoryModel"/>.</returns>
        Task<List<SCRMSubCategoryModel>> GetAllActiveSubCategoryAsync();

        /// <summary>
        /// Gets the sub category by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMSubCategoryModel"/>.</returns>
        Task<SCRMSubCategoryModel> GetSubCategoryByIdAsync(int id);

        /// <summary>
        /// Adds the sub category async.
        /// <para>
        ///     Gets the model of type <see cref="SCRMSubCategoryModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMSubCategoryModel"/>.</returns>
        Task<SCRMSubCategoryModel> AddSubCategoryAsync(SCRMSubCategoryModel model);

        /// <summary>
        /// Updates the sub category async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMSubCategoryModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMSubCategoryModel"/>.</returns>
        Task<SCRMSubCategoryModel> UpdateSubCategoryAsync(int id, SCRMSubCategoryModel model);

        /// <summary>
        /// Updates the sub category status async.
        ///    <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateSubCategoryStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the sub category async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteSubCategoryAsync(int id);

        /// <summary>
        /// Gets the sub category wise template list async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMSubCategroyWiseTemplateModel"/>.</returns>
        Task<List<SCRMSubCategroyWiseTemplateModel>> GetSubCategoryWiseTemplateListAsync(int id);

        /// <summary>
        /// Gets the all sub category wise template list async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMSubCategroyWiseTemplateModel"/>.</returns>
        Task<List<SCRMSubCategroyWiseTemplateModel>> GetAllSubCategoryWiseTemplateListAsync();

        /// <summary>
        /// Gets the category wise sub category.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify SubCategory record.
        /// </para>
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMCategoryWiseSubCategory"/>.</returns>
        Task<SCRMCategoryWiseSubCategory> GetCategoryWiseSubCategory(int categoryId);
    }
}
