using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMAlign"/>.
    /// </summary>
    public interface ISCRMAlignRepository
    {
        /// <summary>
        ///     An asynchronous method to Get All the Align record that exists into the database.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMAlign"/>.</returns>
        Task<List<SCRMAlign>> GetAllAlignAsync();

        /// <summary>
        ///     An asynchronous method to Get Specify Align record that exists into the database.
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of the Specify Align value.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMAlign"/>.</returns>
        Task<SCRMAlign?> GetAlignByIdAsync(int id);

        /// <summary>
        ///     An asynchronous Method to Add Align value in Database.
        /// <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMAlign"/>.
        /// </para>
        /// </summary>
        /// <param name="Fields">Gets A <see cref="{T}"/> of objects of Type <see cref="SCRMAlign"/>.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="{T}"/> where <c>T</c> is <see cref="SCRMAlign"/>.</returns>
        Task<SCRMAlign> AddAlignAsync(SCRMAlign model);

        /// <summary>
        ///     An asynchronous Method to Update Align value in Database.
        /// <para>
        ///     Gets the <see cref="{T}"/> of objects of Type <see cref="SCRMAlign"/>.
        /// </para>
        /// </summary>
        /// <param name="Fields">Gets A <see cref="{T}"/> of objects of Type <see cref="SCRMAlign"/>.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="{T}"/> where <c>T</c> is <see cref="SCRMAlign"/>.</returns>
        Task<SCRMAlign> UpdateAlignAsync(SCRMAlign model);

        /// <summary>
        ///     An asynchronous Method to Delete Align value in Database.
        /// <para>
        ///     Gets the <see cref="id"/> Id of Specify Align.
        /// </para>
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of Align Value that will be delete.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteAlignAsync(int id);
    }
}