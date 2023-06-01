using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class WCMSProductsController : ControllerBase
    {
        private readonly IWCMSProductService _productService;

        public WCMSProductsController(IWCMSProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        ///     A Get api endpoint to get all the Products for a peritcular User.
        /// </summary>
        /// <returns>Returns a Http Response that contains the <see cref="Array"/> of <see cref="WCMSCategoryWiseProductsModel"/> objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductsByCategory([FromQuery] int categoryId)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productService.GetAllProductsByCategoryAsync(categoryId);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A GET api endpoint to get a perticular Product record.
        ///     <para>
        ///         Gets The <see cref="int"/> Id of the record from the query parameters.
        ///     </para>
        /// </summary>
        /// <param name="id">Gets The <see cref="int"/> Id of the record</param>
        /// <returns>Returns a HTTP Response that contains product object at the provided Id.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productService.GetProductByIdAsync(id);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A POST api endpoint To save new product record to the database.
        ///     <para>
        ///         Gets an object of the product reord of type <see cref="WCMSCategoryWiseProductsModel"/>
        ///     </para>
        /// </summary>
        /// <param name="productModel">Gets an object of the product reord of type <see cref="WCMSCategoryWiseProductsModel"/></param>
        /// <returns>Returns the saved record in the HTTP Response in the form of <see cref="WCMSCategoryWiseProductsModel"/></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] WCMSCategoryWiseProductsModel productModel)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productService.AddProductAsync(productModel);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A PUT api endpoint To Update existing product record to the database.
        ///     <para>
        ///         Gets an object of the product reord of type <see cref="WCMSCategoryWiseProductsModel"/>
        ///     </para>
        /// </summary>
        /// <param name="productModel">Gets an object of the product reord of type <see cref="WCMSCategoryWiseProductsModel"/></param>
        /// <returns>Returns the updated Rrecord in the HTTP Response in the form of <see cref="WCMSCategoryWiseProductsModel"/></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] WCMSCategoryWiseProductsModel productModel)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productService.UpdateProductAsync(productModel);
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
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productService.DeleteProductByIdAsync(id);
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
