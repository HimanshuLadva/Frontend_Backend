using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSProductFieldsRepository
    {
        /// <summary>
        /// <para>
        /// An asynchronous method to Add Product Field Type record into the database.
        /// </para>
        /// <para>
        /// Gets the <see cref="List{T}"/> where <c>T</c> is <see cref="WCMSProductFields"/>
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        /// Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="Fields">Gets the <see cref="List{T}"/> of objects of type <see cref="WCMSProductFields"/> to Add.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> AddProductFieldsAsync(List<WCMSProductFields> Fields);
        /// <summary>
        /// <para>
        /// An asynchronous method to Remove Product Field Type record from the database.
        /// </para>
        /// <para>
        /// Gets the <see cref="int"/> Id of the record to Delete.
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        /// Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="id">Gets the <see cref="int"/> id of the Record to Delete.</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteProductFieldAsync(int id);
        /// <summary>
        /// <para>
        /// An asynchronous method to Get All the Product Field Type record that exists into the database.
        /// </para>
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="WCMSProductFields"/>.</returns>
        Task<List<WCMSProductFields>> GetAllProductFieldsAsync();
        /// <summary>
        /// <para>
        /// An asynchronous method to Update Product Field Type record that already exists into the database.
        /// </para>
        /// <para>
        /// Gets the <see cref="WCMSProductFields"/> Field object to Update
        /// </para>
        /// <permission cref="System.Security.PermissionSet">
        /// Permissions - Only Admins of this Web Project can accss this method.
        /// </permission>
        /// </summary>
        /// <param name="Fields">Gets the <see cref="WCMSProductFields"/> Field object to Update</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="WCMSProductFields"/> object of the updated Record.</returns>
        Task<WCMSProductFields?> UpdateProductFieldAsync(WCMSProductFields Field);
    }
}