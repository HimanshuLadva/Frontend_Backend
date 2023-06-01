using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class WCMSProductFieldController : ControllerBase
    {
        private readonly IWCMSProductFieldService _productFieldService;
        public WCMSProductFieldController(IWCMSProductFieldService productFieldService)
        {
            _productFieldService = productFieldService;
        }
        /// <summary>
        ///     A GET api endpoint to get all the Product Fields for a peritcular User.
        /// </summary>
        /// <returns>Returns a Http Response that contains the <see cref="Array"/> of <see cref="WCMSProductFieldsModel"/> objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductFields()
        {
            WCMSResponse response = new();
            try
            {
                var result = await _productFieldService.GetAllProductFieldsAsync();
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
            
            
        }
        /// <summary>
        ///     A POST api endpoint to Add Product Fields.
        ///     <para>
        ///         Gets <see cref="List{T}"/> of objercts of type <see cref="WCMSProductFieldsModel"/>
        ///     </para>
        ///     <permission cref="System.Security.PermissionSet">
        ///         Permissions - Only Admins of this Web Project can accss this method.
        ///     </permission>
        /// </summary>
        /// <param name="models">Gets <see cref="List{T}"/> of objercts of type <see cref="WCMSProductFieldsModel"/></param>
        /// <returns>Returns a Http Response that contains the <see cref="Boolean"/> indicating success of the operatiuon.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProductField([FromBody] List<WCMSProductFieldsModel> models)
        {
            
            WCMSResponse response = new();
            try
            {
                var result = await _productFieldService.AddProductFieldsAsync(models);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A POST api endpoint to Update Product Fields.
        ///     <para>
        ///         Gets <see cref="List{T}"/> of objercts of type <see cref="WCMSProductFieldsModel"/>
        ///     </para>
        ///     <permission cref="System.Security.PermissionSet">
        ///         Permissions - Only Admins of this Web Project can accss this method.
        ///     </permission>
        /// </summary>
        /// <param name="models">Gets <see cref="List{T}"/> of objercts of type <see cref="WCMSProductFieldsModel"/></param>
        /// <returns>Returns a Http Response that contains the Updated Record of type <see cref="WCMSProductFieldsModel"/>.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProductField([FromForm] WCMSProductFieldsModel model)
        {
            
            WCMSResponse response = new();
            try
            {
                var result = await _productFieldService.UpdateProductFieldsAsync(model);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return StatusCode(response.status, response);
        }
        /// <summary>
        ///     A POST api endpoint to Add Product Fields.
        ///     <para>
        ///         Gets <see cref="int"/> Id of field type record.
        ///     </para>
        ///     <permission cref="System.Security.PermissionSet">
        ///         Permissions - Only Admins of this Web Project can accss this method.
        ///     </permission>
        /// </summary>
        /// <param name="id">Gets <see cref="int"/> Id of field type record.</param>
        /// <returns>Returns a Http Response that contains the <see cref="Boolean"/> indicating success of the operatiuon.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProductField([FromQuery] int id)
        {
            
            WCMSResponse response = new();
            try
            {
                var result = await _productFieldService.DeleteProductFieldByIdAsync(id);
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
