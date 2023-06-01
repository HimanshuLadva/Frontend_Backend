using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWCMSProductFieldService
    {
        /// <summary>
        /// <para>
        ///     An asynchronous method to Map Product Field Type record into the Entity of the Database.
        /// </para>
        /// <para>
        /// Gets the <see cref="List{T}"/> where <c>T</c> is <see cref="WCMSProductFieldsModel"/>
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        ///     Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="models">Gets the <see cref="List{T}"/> of objects of type <see cref="WCMSProductFieldsModel"/> to Add.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> AddProductFieldsAsync(List<WCMSProductFieldsModel> models);
        /// <summary>
        /// <para>
        /// An asynchronous method to Remove Product Field Type record from the database.
        /// </para>
        /// <para>
        /// Gets the <see cref="int"/> Id of the record to Delete.
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        ///     Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="id">Gets the <see cref="int"/> id of the Record to Delete.</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteProductFieldByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get all the product field type records and Update it accordingly as per need.
        /// </para>
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="WCMSProductFieldsModel"/>.</returns>
        Task<List<WCMSProductFieldsViewModel>> GetAllProductFieldsAsync();
        /// <summary>
        /// <para>
        ///     An asynchronous method to Map the model od Product Field Type into Database Entity record that already exists into the database and Update it.
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSProductFieldsModel"/> Field object to Update
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        ///     Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="model">Gets the <see cref="WCMSProductFieldsModel"/> Field object to Update</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="WCMSProductFieldsModel"/> object of the updated Record.</returns>
        Task<WCMSProductFieldsModel> UpdateProductFieldsAsync(WCMSProductFieldsModel model);
    }
}