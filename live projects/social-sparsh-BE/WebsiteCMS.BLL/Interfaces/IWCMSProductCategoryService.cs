using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.Data.Interface;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWCMSProductCategoryService
    {
        ///
        /// <summary>
        ///     <para>
        ///         An asynchronous method to Map Model with category Entity and save into the database.
        ///     </para>
        ///     <para>
        ///         Gets the <see cref="WCMSProductCategoriesModel"/> category object.
        ///     </para>
        ///     <para>
        ///         Calls <see cref="IBaseRepository.GetUserId();"/> To Get Details of the user and then Passes Id of the User to <see cref="IWCMSProductCategoryRepository.AddCategoryAsync(WCMSProductCategories)"/> To save The Entity.
        ///     </para>
        /// </summary>
        /// <param name="model">The object instance of <see cref="WCMSProductCategoriesModel"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Saved Record object of type <see cref="WCMSProductCategoriesModel"/></returns>
        Task<WCMSProductCategoriesModel> AddCategoriesAsync(WCMSProductCategoriesModel model);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Remove category record and all the Products related to it from the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> Id category record.
        /// </para>
        /// <para>
        ///     This will also Delete All the product records from the database.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductCategoryRepository.DeleteCategoryByIdAsync(int)"/> To Delete.
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> Id of the category to Delete.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> that indicates Success or Failuer</returns>
        Task<bool> DeleteCategoryByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get all the category records of a user and Update it accordingly as per need.
        /// </para>
        /// <para>
        ///     Calls <see cref="IBaseRepository.GetUserId();"/> To Get Details of the user and then Passes Id of the User to <see cref="IWCMSProductCategoryRepository.GetAllCategoriesByUserAsync(string)"/>
        /// </para>
        /// <para>
        ///     The product records are mapped to in 
        /// <code>
        ///     <see cref="ICollection{WCMSCategoryWiseProducts}?"/> Products where <see cref="T"/> is <see cref="WCMSCategoryWiseProductsModel"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <returns>Returns  <see cref="Task"/> of <see cref="List{T}?"/> where T is <seealso cref="WCMSProductCategoriesModel"/></returns>
        Task<List<WCMSProductCategoriesModel>> GetAllCategoriesAsync();
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get the category record by Id and Update it accordingly as per need.
        /// </para>
        /// <para>
        ///     Gets <see cref="int"/> Id of the Category record.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductCategoryRepository.GetCategoryByIdAsync(int)"/> To find the record from database.
        /// </para>
        /// <para>
        ///     The product records are mapped to in:
        /// <code>
        ///     <see cref="ICollection{WCMSCategoryWiseProducts}?"/> Products where <see cref="T"/> is <see cref="WCMSProductCategoriesModel"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="id">Gets <see cref="int"/> Id of the Category record.</param>
        /// <returns>Returns  <see cref="Task"/> of <seealso cref="WCMSProductCategoriesModel"/></returns>
        Task<WCMSProductCategoriesModel> GetCategoryByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Map Model with category Entity and Update the value of the record That already exist.
        /// </para>
        /// <para>
        ///     Gets the updated <see cref="WCMSProductCategoriesModel"/> category object of record.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductCategoryRepository.UpdateCategoryAsync(WCMSProductCategories)"/> To find the record from database and Update it..
        /// </para>
        /// </summary>
        /// <param name="model">The object of the <see cref="WCMSProductCategoriesModel"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Updated Record object of type <see cref="WCMSProductCategoriesModel"/></returns>
        Task<WCMSProductCategoriesModel> UpdateCategoryAsync(WCMSProductCategoriesModel model);
    }
}