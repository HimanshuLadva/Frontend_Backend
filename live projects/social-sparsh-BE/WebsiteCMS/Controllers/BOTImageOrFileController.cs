using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BOTImageOrFileController : ControllerBase
    {
        private readonly IBOTImageOrFileService _imageOrFileService;

        public BOTImageOrFileController(IBOTImageOrFileService imageOrFileService)
        {
            _imageOrFileService = imageOrFileService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImageOrFile([FromForm] BOTImageOrFileModel model)
        {
            BOTResponse response = new();
            if (!ModelState.IsValid)
            {
                response.status = StatusCodes.Status400BadRequest;
            }
            else
            {
                try
                {
                    var imageOrFile = await _imageOrFileService.UpdateImageOrFile(model);
                    response.success = imageOrFile;
                    response.status = StatusCodes.Status201Created;
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                catch (CustomDBException e)
                {
                    await e.LogException(ControllerContext);
                    response = response.ExceptionResult(e);
                }
            }
            return StatusCode(response.status, response);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImageOrFile([FromQuery] string FrontendId)
        {
            BOTResponse response = new();
            try
            {
                var data = await _imageOrFileService.GetImageOrFileByFrontendId(FrontendId);
                if (data != null)
                {
                    await _imageOrFileService.DeleteImageOrFile(data.FrontendId!);
                    response.data = $"Requested Template Field Type for Id = {data.Id} is Deleted Successfully...!";
                    response.success = true;
                    response = response.ActionResultData(new { id = data.Id }, StatusCodes.Status200OK);
                }
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);
            }
            return StatusCode(response.status, response);
        }
    }
}
