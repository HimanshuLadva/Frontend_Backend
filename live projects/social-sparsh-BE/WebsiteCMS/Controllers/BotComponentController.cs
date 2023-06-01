using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteCMS.Controllers
{

    [Route("[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class BOTComponentController : ControllerBase
    {
        private readonly IBOTComponentService _componentService;
        public BOTComponentController(IBOTComponentService componentService)
        {
            _componentService = componentService;
        }
        [HttpPost]
        public async Task<IActionResult> AddComponent([FromForm] BOTComponentModel model)
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
                    var componentId = await _componentService.AddComponentAsync(model);
                    response = response.ActionResultData(componentId, StatusCodes.Status201Created);
                }
                catch (CustomDBException e)
                {
                    await e.LogException(ControllerContext);
                    response = response.ExceptionResult(e);

                }
            }
            return StatusCode(response.status, response);
        }

        [HttpPut]
        public async Task<IActionResult> EditComponent([FromForm] BOTComponentModel model)
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
                    var componentId = await _componentService.EditComponentAsync(model);
                    response = response.ActionResultData(componentId, StatusCodes.Status201Created);
                }
                catch (CustomDBException e)
                {
                    await e.LogException(ControllerContext);
                    response = response.ExceptionResult(e);

                }
            }
            return StatusCode(response.status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComponentTypes()
        {
            BOTResponse response = new();
            try
            {
                var components = await _componentService.GetAllComponentsAsync();
                response = response.ActionResultData(components, StatusCodes.Status200OK);
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
