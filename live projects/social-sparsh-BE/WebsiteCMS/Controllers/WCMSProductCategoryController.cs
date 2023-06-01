using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.Controllers
{
    /// <summary>
    /// The <see cref="class"/> that deal with the HTTP requests related to <see cref="WCMSProductCategoriesModel"/>.
    /// </summary>
    [Route("[controller]/[action]")]
    [Authorize]
    public class WCMSProductCategoryController : ControllerBase
    {
        private readonly IWCMSProductCategoryService _productCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WCMSProductCategoryController"/> class.
        /// </summary>
        /// <param name="productCategoryService">The product category service.</param>
        public WCMSProductCategoryController(IWCMSProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        /// <summary>
        ///     A Get api endpoint to get all the Categories for a peritcular User.
        /// </summary>
        /// <returns>Returns a Http Response that contains the <see cref="Array"/> of category objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productCategoryService.GetAllCategoriesAsync();
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A GET api endpoint to get all the Categories for a peritcular User.
        ///     <para>
        ///         Gets The <see cref="int"/> Id of the record from the query parameters.
        ///     </para>
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of the record</param>
        /// <returns>Returns a HTTP Response that contains category object at the provided Id.</returns>
        [HttpGet]
        public async Task<IActionResult> GeCategoryById([FromQuery] int id)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productCategoryService.GetCategoryByIdAsync(id);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A POST api endpoint To save new category record to the database.
        ///     <para>
        ///         Gets an object of the category reord of type <see cref="WCMSProductCategoriesModel"/>
        ///     </para>
        /// </summary>
        /// <param name="model">Gets an object of the category reord of type <see cref="WCMSProductCategoriesModel"/></param>
        /// <returns>Returns the saved record in the HTTP Response in the form of <see cref="WCMSProductCategoriesModel"/></returns>
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] WCMSProductCategoriesModel model)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productCategoryService.AddCategoriesAsync(model);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A PUT api endpoint To Update existing category record to the database.
        ///     <para>
        ///         Gets an object of the category reord of type <see cref="WCMSProductCategoriesModel"/>
        ///     </para>
        /// </summary>
        /// <param name="model">Gets an object of the category reord of type <see cref="WCMSProductCategoriesModel"/></param>
        /// <returns>Returns the updated Rrecord in the HTTP Response in the form of <see cref="WCMSProductCategoriesModel"/></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] WCMSProductCategoriesModel model)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productCategoryService.UpdateCategoryAsync(model);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A DELETE api endpoint To Update existing category record to the database.
        ///     <para>
        ///         Gets The <see cref="int"/> Id of the record from the query parameters.
        ///     </para>
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of the record To Delete.</param>
        /// <returns>Returns a HTTP Response that contains category object ad the provided Id.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] int id)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productCategoryService.DeleteCategoryByIdAsync(id);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }

    }
}
