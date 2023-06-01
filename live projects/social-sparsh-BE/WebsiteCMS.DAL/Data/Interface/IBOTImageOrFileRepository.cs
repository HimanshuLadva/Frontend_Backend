using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="BOTImageOrFile"/>.
    /// </summary>
    public interface IBOTImageOrFileRepository
    {

        /// <summary>
        /// Updates the image or file.
        /// <para>
        ///     Gets the model of type <see cref="BOTImageOrFileModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateImageOrFile(BOTImageOrFileModel model);

        /// <summary>
        /// Deletes the image or file.
        /// <para>
        ///     Gets The <see cref="string"/> FrontendId of the Specify Question record.
        /// </para>
        /// </summary>
        /// <param name="FrontendId">The frontend id.</param>
        /// <returns>A Task.</returns>
        Task DeleteImageOrFile(string FrontendId);

        /// <summary>
        /// Gets the image or file by frontend id.
        ///  <para>
        ///     Gets The <see cref="string"/> FrontendId of the Specify Question record.
        /// </para>
        /// </summary>
        /// <param name="FrontendId">The frontend id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BOTImageOrFile"/>.</returns>
        Task<BOTImageOrFile> GetImageOrFileByFrontendId(string FrontendId);

        /// <summary>
        /// Adds the imageor file.
        ///  <para>
        ///     Gets the model of type <see cref="BOTImageOrFile"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task AddImageorFile(BOTImageOrFile model);

        /// <summary>
        /// Deletes the other frontend id.
        /// </summary>
        /// <returns>A Task.</returns>
        Task DeleteOtherFrontendId();
    }
}
