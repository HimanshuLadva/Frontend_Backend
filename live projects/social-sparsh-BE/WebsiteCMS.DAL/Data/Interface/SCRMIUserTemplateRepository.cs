using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMUserTemplateModel"/>.
    /// </summary>
    public interface SCRMIUserTemplateRepository
    {
        /// <summary>
        /// Gets the all user template async.
        ///    <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="SCRMUserTemplateModel"/>.</returns>
        Task<IPagedList<SCRMUserTemplateModel>> GetAllUserTemplateAsync(string userId, SCRMRequestParams requestParams);

        /// <summary>
        /// Gets the user template by id async.
        ///     <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        ///     <para>
        ///     Gets The <see cref="int"/> templateId is the Specify Template record.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMUserTemplateModel"/>.</returns>
        Task<SCRMUserTemplateModel> GetUserTemplateByIdAsync(string userId, int templateId);
    }
}
