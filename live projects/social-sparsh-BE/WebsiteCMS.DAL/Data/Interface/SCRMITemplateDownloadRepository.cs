using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="FileContentResult"/>.
    /// </summary>
    public interface SCRMITemplateDownloadRepository
    {
        /// <summary>
        /// Downloads the user template by id async.
        ///   <para>
        ///     Gets The <see cref="string"/> userId of the Specify User.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="int"/> templateId of the Specify Template record.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="float"/> templateWidth is width of template.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="float"/> templateHeight is width of template.
        /// </para>
        ///    <para>
        ///     Gets the model of type <see cref="IHeaderDictionary"/>.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateWidth">The template width.</param>
        /// <param name="templateHeight">The template height.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="FileContentResult"/>.</returns>
        Task<FileContentResult> DownloadUserTemplateByIdAsync(string userId, int templateId, float templateWidth, float templateHeight, IHeaderDictionary model);
    }
}
