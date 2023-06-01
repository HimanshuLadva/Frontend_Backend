using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMFontFamilyModel"/>.
    /// </summary>
    public interface SCRMIFontFamilyRepository
    {
        /// <summary>
        /// Gets the all font family async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMFontFamilyModel"/>.</returns>
        Task<List<SCRMFontFamilyModel>> GetAllFontFamilyAsync();

        /// <summary>
        /// Gets the font family by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify FontFamily record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMFontFamilyModel"/>.</returns>
        Task<SCRMFontFamilyModel> GetFontFamilyByIdAsync(int id);

        /// <summary>
        /// Adds the font family async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMFontFamilyModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMFontFamilyModel"/>.</returns>
        Task<SCRMFontFamilyModel> AddFontFamilyAsync(SCRMFontFamilyModel model);

        /// <summary>
        /// Updates the font family async.
        ///   <para>
        ///     Gets The <see cref="int"/> Id of the Specify FontFamily record.
        /// </para>
        ///    <para>
        ///     Gets the model of type <see cref="SCRMFontFamilyModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMFontFamilyModel"/>.</returns>
        Task<SCRMFontFamilyModel> UpdateFontFamilyAsync(int id, SCRMFontFamilyModel model);

        /// <summary>
        /// Updates the font family status async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify FontFamily record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateFontFamilyStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the font family async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify FontFamily record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteFontFamilyAsync(int id);
    }
}
