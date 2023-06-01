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
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMBackgroundColorModel"/>.
    /// </summary>
    public interface SCRMIBackgroundColorRepository
    {
        /// <summary>
        ///     An asynchronous method to Get All the Color record that exists into the database.
        ///  <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMBackgroundColorModel"/>.</returns>
        Task<IPagedList<SCRMBackgroundColorModel>> GetAllColorsAsync(SCRMRequestParams requestParams);

        /// <summary>
        ///     An asynchronous method to Get specify Color record that exists into the database.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Color record.
        /// </para>
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of the Specify Color value.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="{T}"/> where <c>T</c> is <see cref="SCRMBackgroundColorModel"/>.</returns>
        Task<SCRMBackgroundColorModel> GetBackgroundColorByIdAsync(int id);

        /// <summary>
        ///     An asynchronous Method to Add Color value in Database.
        /// <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMBackgroundColorModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">Gets A <see cref="{T}"/> of objects of Type <see cref="SCRMBackgroundColorModel"/>.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="{T}"/> where <c>T</c> is <see cref="SCRMBackgroundColorModel"/>.</returns>
        Task<SCRMBackgroundColorModel> AddBackgroundColorAsync(SCRMBackgroundColorModel model);
    }
}
