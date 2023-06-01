using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="BusinessCategoryModel"/>.
    /// </summary>
    public interface IBusinessCategoryService
    {
        /// <summary>
        /// Gets the category by id async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BusinessCategoryModel"/>.</returns>
        Task<BusinessCategoryModel> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Adds the category.
        ///    <para>
        ///     Gets the model of type <see cref="BusinessCategoryModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BusinessCategoryModel"/>.</returns>
        Task<BusinessCategoryModel> AddCategory(BusinessCategoryModel model);

        /// <summary>
        /// Gets the all category async.
        ///  <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="BusinessCategoryModel"/>.</returns>
        Task<IPagedList<BusinessCategoryModel>> GetAllCategoryAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Updates the category async.
        ///    <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        ///    <para>
        ///     Gets The <see cref="string"/> Name Specify New Name for Category.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Name">The name.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BusinessCategoryModel"/>.</returns>
        Task<BusinessCategoryModel> UpdateCategoryAsync(int id, string Name);

        /// <summary>
        /// Deletes the category async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteCategoryAsync(int id);
    }
}
