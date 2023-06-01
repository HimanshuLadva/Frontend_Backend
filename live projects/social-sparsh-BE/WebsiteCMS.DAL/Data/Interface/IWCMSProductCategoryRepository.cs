using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSProductCategoryRepository
    {
        /// <summary>
        /// <para>
        ///     An asynchronous method to Add category record and save into the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSProductCategories"/> category object.
        /// </para>
        /// </summary>
        /// <param name="category">The object instance of <see cref="WCMSProductCategories"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Saved Record object of type <see cref="WCMSProductCategories"/></returns>
        Task<WCMSProductCategories> AddCategoryAsync(WCMSProductCategories category);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Remove category record and all the Products related to it from the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> Id category record.
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> Id of the category to Delete.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> that indicates Success or Failuer</returns>
        Task<bool> DeleteCategoryByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get all the category records of a user from database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> Id of the user.
        /// </para>
        /// <para>
        ///     The product records are mapped to in 
        /// <code>
        ///     <see cref="ICollection{WCMSCategoryWiseProducts}?"/> Products where <see cref="T"/> is <see cref="WCMSCategoryWiseProducts"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="UserId">The Id of The user in string format.</param>
        /// <returns>Returns  <see cref="Task"/> of <see cref="List{T}?"/> where T is <seealso cref="WCMSProductCategories"/></returns>
        Task<List<WCMSProductCategories>?> GetAllCategoriesByUserAsync(string UserId);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get category record and the product records related to it from database.
        /// <para>
        /// <para>
        ///     Gets the <see cref="int"/> Id of the Category record.
        /// </para>
        ///     The product records are mapped to in 
        /// </para>
        /// <code>
        ///     <see cref="ICollection{WCMSCategoryWiseProducts}?"/> Products where <see cref="T"/> is <see cref="WCMSCategoryWiseProducts"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> Id of the category</param>
        /// <returns>Returns  <see cref="Task"/> of <see cref="WCMSProductCategories"/></returns>
        Task<WCMSProductCategories?> GetCategoryByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Update category record that already exists in the database.
        /// </para>
        /// <para>
        ///     Gets the updated <see cref="WCMSProductCategories"/> category object of record.
        /// </para>
        /// </summary>
        /// <param name="model">The object of the <see cref="WCMSProductCategories"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Updated Record object of type <see cref="WCMSProductCategories"/></returns>
        Task<WCMSProductCategories> UpdateCategoryAsync(WCMSProductCategories model);
    }
}