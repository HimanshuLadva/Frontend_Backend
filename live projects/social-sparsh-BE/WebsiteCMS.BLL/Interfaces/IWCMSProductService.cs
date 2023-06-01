using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Interface;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWCMSProductService
    {
        /// <summary>
        /// <para>
        ///     An asynchronous method to Map Model with Product Entity and save into the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSCategoryWiseProductsModel"/> category object.
        /// </para>
        /// <para>
        ///     This will also add the <see cref="WCMSUserProductFields"/> related to the product.
        /// </para>
        ///     <para>
        ///         Calls <see cref="IWCMSProductRepository.AddProductAsync(WCMSCategoryWiseProducts)"/> To save The Entity.
        ///     </para>
        /// </summary>
        /// <param name="model">The object instance of <see cref="WCMSCategoryWiseProductsModel"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Saved Record object of type <see cref="WCMSCategoryWiseProductsModel"/></returns>
        Task<WCMSCategoryWiseProductsModel> AddProductAsync(WCMSCategoryWiseProductsModel model);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Remove Product record and all the Fields and their Values related to it from the database.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> Id Product record.
        /// </para>
        /// <para>
        ///     This will also delete <see cref="WCMSUserProductFields"/> related to the product.
        /// </para>
        /// <para>
        ///     This will also Delete All the product records from the database.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductRepository.DeleteProductByIdAsync(int)"/> To Delete.
        /// </para>
        /// </summary>
        /// <param name="id">The <see cref="int"/> Id of the category to Delete.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> that indicates Success or Failur.</returns>
        Task<bool> DeleteProductByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get the Product record by Id and Update it accordingly as per need.
        /// </para>
        /// <para>
        ///     Gets <see cref="int"/> Id of the Product record.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductRepository.GetProductByIdAsync(int)"/> To find the record from database.
        /// </para>
        /// <para>
        ///     The product records are mapped to in:
        /// <code>
        ///     <see cref="List{T}?"/> Fields and their Values where <see cref="T"/> is <see cref="WCMSCategoryWiseProductsModel"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="CategoryId">Gets <see cref="int"/> Id of the Product record.</param>
        /// <returns>Returns  <see cref="Task"/> of <seealso cref="WCMSCategoryWiseProductsModel"/></returns>
        Task<List<WCMSCategoryWiseProductsModel>> GetAllProductsByCategoryAsync(int CategoryId);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Get the Product record by Id and Update it accordingly as per need.
        /// </para>
        /// <para>
        ///     Gets <see cref="int"/> Id of the Product record.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductRepository.GetProductByIdAsync(int)"/> To find the record from database.
        /// </para>
        /// <para>
        ///     The product records are mapped to in:
        /// <code>
        ///     <see cref="ICollection{T}?"/> Fields where <see cref="T"/> is <see cref="WCMSUserProductFieldsModel"/>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="id">Gets <see cref="int"/> Id of the Product record.</param>
        /// <returns>Returns  <see cref="Task"/> of <seealso cref="WCMSProductCategories"/></returns>
        Task<WCMSCategoryWiseProductsModel> GetProductByIdAsync(int id);
        /// <summary>
        /// <para>
        ///     An asynchronous method to Map Model with Product Entity and Update the value of the record That already exist.
        /// </para>
        /// <para>
        ///     This will also add update or delete <see cref="WCMSUserProductFields"/> related to the product.
        /// </para>
        /// <para>
        ///     Gets the updated <see cref="WCMSCategoryWiseProductsModel"/> category object of record.
        /// </para>
        /// <para>
        ///     Calls <see cref="IWCMSProductRepository.UpdateProductAsync(WCMSCategoryWiseProducts)"/> To find the record from database and Update it.
        /// </para>
        /// </summary>
        /// <param name="model">The object of the <see cref="WCMSCategoryWiseProductsModel"/></param>
        /// <returns>Returns  <see cref="Task"/> of the Updated Record object of type <see cref="WCMSCategoryWiseProductsModel"/></returns>
        Task<WCMSCategoryWiseProductsModel> UpdateProductAsync(WCMSCategoryWiseProductsModel model);
    }
}