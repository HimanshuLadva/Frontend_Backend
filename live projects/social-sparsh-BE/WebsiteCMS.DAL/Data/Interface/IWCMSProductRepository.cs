using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSProductRepository
    {
        Task<List<WCMSCategoryWiseProducts>> GetAllProductsByCategoryAsync(int CategoryId);
        Task<WCMSCategoryWiseProducts?> GetProductByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous Method to Add Product Records and the Field Values For its Fields.
        /// </para>
        /// <para>
        ///     Gets an object of <see cref="WCMSCategoryWiseProducts"/>
        /// </para>
        /// </summary>
        /// <param name="product">Gets an object of <see cref="WCMSCategoryWiseProducts"/></param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="WCMSCategoryWiseProducts"/> that was Saved. </returns>
        Task<WCMSCategoryWiseProducts?> AddProductAsync(WCMSCategoryWiseProducts product);
        /// <summary>
        /// <para>
        ///     An asynchronous Method to Update Product Record and the Field Values For its Fields That Already Exists into the database.
        /// </para>
        /// <para>
        ///     If There are New Fields In the <see cref="ICollection{T}"/> Fields Than Newer field values will be saved.
        /// </para>
        /// <para>
        ///     Gets an object of <see cref="WCMSCategoryWiseProducts"/>
        /// </para>
        /// </summary>
        /// <param name="product">Gets an object of <see cref="WCMSCategoryWiseProducts"/></param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="WCMSCategoryWiseProducts"/> that was Saved. </returns>
        Task<WCMSCategoryWiseProducts> UpdateProductAsync(WCMSCategoryWiseProducts product);
        /// <summary>
        /// <para>
        ///     An asynchronous Method to Add Product Records and the Field Values For its Fields.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> id of the Record to Delete.
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> id of the Record to Delete.</param>
        /// <returns>Returns a  <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteProductByIdAsync(int id);
    }
}