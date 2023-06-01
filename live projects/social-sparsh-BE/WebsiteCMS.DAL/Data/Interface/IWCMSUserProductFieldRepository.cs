using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="WCMSUserProductFields"/>.
    /// </summary>
    public interface IWCMSUserProductFieldRepository
    {
        /// <summary>
        /// <para>
        ///     An asynchronous Method to Add values of multiple Fields of a product.
        /// </para>
        /// <para>
        ///     Gets A <see cref="List{T}"/> of objects of Type <see cref="WCMSUserProductFields"/>.
        /// </para>
        /// </summary>
        /// <param name="Fields">Gets A <see cref="List{T}"/> of objects of Type <see cref="WCMSUserProductFields"/>.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> AddUserProductFieldsAsync(List<WCMSUserProductFields> Fields);
        /// <summary>
        /// <para>
        ///     An asynchronous method to delete a field value of a product from the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> Id of the Field Reccord to be Deleted.
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> id of the Record to Delete.</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteUserProductFieldAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get All the Product Field Type record that exists into the database.
        /// </para>
        /// <para>
        ///     Gets The <see cref="int"/> Id of the Product related to the field value.
        /// </para>
        /// </summary>
        /// <param name="productId">Gets The <see cref="int"/> Id of the Product related to the field value.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="WCMSUserProductFields"/>.</returns>
        Task<List<WCMSUserProductFields>> GetAllUserFieldsAsync(int productId);
    }
}