using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    public class ErrorController : Controller
    {
        //[HttpGet("{*url}", Order = int.MaxValue)]
        public IActionResult ErrorHandle()
        {
            SCRMResponse response = new();
            try
            {
                response.success = true;
                response.error = "Sorry, The Page You Are Looking For Is Not Found...!";

                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
