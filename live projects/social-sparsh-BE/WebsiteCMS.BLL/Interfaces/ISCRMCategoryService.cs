using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMCategoryModel"/>.
    /// </summary>
    public interface ISCRMCategoryService
    {
        /// <summary>
        /// Gets the category wise template list async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Category record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMCategroyWiseTemplateModel"/>.</returns>
        Task<List<SCRMCategroyWiseTemplateModel>> GetCategoryWiseTemplateListAsync(int id);

    }
}
