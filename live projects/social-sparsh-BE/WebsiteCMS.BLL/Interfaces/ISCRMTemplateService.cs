using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTemplateMetadateAndLayoutModel"/>.
    /// </summary>
    public interface ISCRMTemplateService
    {
        /// <summary>
        /// Templates the metadate and layout by id async.
        ///   <para>
        ///     Gets The <see cref="int"/> templateId Specify template record.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateMetadateAndLayoutModel"/>.</returns>
        Task<SCRMTemplateMetadateAndLayoutModel> TemplateMetadateAndLayoutByIdAsync(int templateId);
    }
}