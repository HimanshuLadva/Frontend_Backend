using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class BOTAvatarController : Controller
    {
        private readonly IBOTAvatarService _AvatarService;
        public BOTAvatarController(IBOTAvatarService AvatarService)
        {
            _AvatarService = AvatarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAvatar()
        {
            BOTResponse response = new();
            try
            {
                var avatars = await _AvatarService.getAvatar();
                response = response.ActionResultData(new { avatars }, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);

            }
            return StatusCode(response.status, response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DaleteAvatar(long Id)
        {
            BOTResponse response = new();
            try
            {
                var id = await _AvatarService.DeleteAvatar(Id);
                response = response.ActionResultData(new { id }, StatusCodes.Status200OK);
            }
            catch (CustomDBException e)
            {
                await e.LogException(ControllerContext);
                response = response.ExceptionResult(e);

            }
            return StatusCode(response.status, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAvatar(BOTAvatarModel model)
        {
            BOTResponse response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var id = await _AvatarService.AddAvatarAsync(model);
                    response = response.ActionResultData(new { id }, StatusCodes.Status200OK);
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
