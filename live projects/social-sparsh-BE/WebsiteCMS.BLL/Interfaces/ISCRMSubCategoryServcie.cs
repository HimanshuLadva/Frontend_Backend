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
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMSubCategoryModel"/>.
    /// </summary>
    public interface ISCRMSubCategoryServcie
    {
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
