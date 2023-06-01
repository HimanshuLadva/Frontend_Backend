using Microsoft.AspNetCore.Mvc;
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
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="BusinessModel"/>.
    /// </summary>
    public interface IBusinessRepository
    {
        /// <summary>
        /// Adds the business.
        /// <para>
        ///     Gets the model of type <see cref="BusinessModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateModel"/>.</returns>
        Task<BusinessModel> AddBusiness(BusinessModel model);

        /// <summary>
        /// Gets the all business detail.
        /// <para>
        ///     Gets the model of type <see cref="SCRMRequestParams"/>.
        /// </para>
        /// </summary>
        /// <param name="requestParams">The request params.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IPagedList{T}"/> where <c>T</c> is <see cref="BusinessModel"/>.</returns>
        Task<IPagedList<BusinessModel>> GetAllBusinessDetail(SCRMRequestParams requestParams);

        /// <summary>
        /// Updates the business detail.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Business Detail record.
        /// </para>
        ///     <para>
        ///     Gets the model of type <see cref="BusinessModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BusinessModel"/>.</returns>
        Task<BusinessModel> UpdateBusinessDetail(int id, BusinessModel model);

        /// <summary>
        /// Gets the business detail by id async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Business Detail record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="BusinessModel"/>.</returns>
        Task<BusinessModel> GetBusinessDetailByIdAsync(int id);

        /// <summary>
        /// Deletes the business detail async.
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Specify Business Detail record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteBusinessDetailAsync(int id);
    }
}
