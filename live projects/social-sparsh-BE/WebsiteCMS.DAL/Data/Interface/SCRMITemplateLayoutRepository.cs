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
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTemplateLayoutModel"/>.
    /// </summary>
    public interface SCRMITemplateLayoutRepository
    {
        /// <summary>
        /// Gets the all template layout async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMTemplateLayoutModel"/>.</returns>
        Task<IPagedList<SCRMTemplateLayoutModel>> GetAllTemplateLayoutAsync(SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the template layout by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> templateId of the Specify template record.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateLayoutModel"/>.</returns>
        Task<SCRMTemplateLayoutModel> GetTemplateLayoutByIdAsync(int templateId);

        /// <summary>
        /// Updates the template layout async.
        ///   <para>
        ///     Gets The <see cref="int"/> templateId of the Specify template record.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTemplateLayoutModel"/>.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateLayoutModel"/>.</returns>
        Task<SCRMTemplateLayoutModel> UpdateTemplateLayoutAsync(int templateId, SCRMTemplateLayoutModel model);
    }
}
