using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using Org.BouncyCastle.Bcpg;
using System;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SCRMSocialController : ControllerBase
    {
        AuthResponse response = new AuthResponse();
        private readonly ISCRMSocialService _socialService;

        public SCRMSocialController(ISCRMSocialService socialService)
        {
            _socialService = socialService;
        }

        [HttpPost("FBPagesAndTokens")]
        public async Task<bool> FBPagesAndTokens([FromBody] TokenModel accessToken)
        {
            bool fbval = await _socialService.FBPagesAndTokens(accessToken);
            if (fbval)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost("GetInstagramAllPostDetail")]
        public async Task<IActionResult> GetInstagramAllPostDetail([FromBody] TokenModel tokenModel)
        {
            var result = await _socialService.GetInstagramAllPostDetail(tokenModel);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                response.Success = false;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpPost("GetInstagramAccountDetail")]
        public async Task<IActionResult> GetInstagramAccountDetail([FromBody] TokenModel tokenModel)
        {
            var result = await _socialService.GetInstaAccountDetail(tokenModel);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                response.Success = false;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpPost("GetInstagramAccountLinkedWithFbPage")]
        public async Task<IActionResult> GetInstagramAccountLinkedWithFbPage([FromBody] TokenModel tokenModel)
        {
            var result = await _socialService.GetInstagramAccountLinkedWithFbPage(tokenModel);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                response.Success = false;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpGet("connected_accounts")]
        public async Task<IActionResult> GetAllFBPageAndLinkedInstagramAccount([FromQuery] string accessToken)
        {
            var result = await _socialService.GetAllFBPageAndLinkedInstagramAccount(accessToken);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                response.Success = false;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpPost("Post/{PageName}/{imagePath}")]
        public async Task<IActionResult> FBPostImage([FromRoute] string PageName, [FromQuery] string Caption, [FromForm] IFormFile imagePath)
        {

            SCRMResponse response = new();
            try
            {
                int post = await _socialService.FBPostImage(PageName, Caption, imagePath);
                if (post != 0)
                {
                    response.data = post;
                    response.success = true;
                    return Ok(response);
                }
                response.error = $"Something went wrong";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("post_to_social_channels")]
        public async Task<IActionResult> PostOnSocialMedialChannel([FromForm] PostOnSocialMediaChannelModel dataModel)
        {
            SCRMResponse response = new();
            try
            {
                var data = await _socialService.PostOnSocialMedialChannel(dataModel);
                response.data = data;
                response.success = true;
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                return StatusCode(400, response);
            }
        }

        [HttpPost("InstaPost/{imagePath}/{caption}")]
        public async Task<bool> IGPostImage([FromBody] TokenModel tokenModel, [FromRoute] string imagePath, [FromRoute] string caption)
        {
            bool isImagePosted = await _socialService.IGPostImage(tokenModel, imagePath, caption);
            if (isImagePosted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("FacebookPageList")]
        public async Task<IActionResult> FacebookPageList()
        {
            SCRMResponse Response = new();
            try
            {
                List<AllFBPageAndLinkedInstagramAccountModel> fbval = await _socialService.FBPageList();
                if (fbval != null)
                {
                    Response.data = fbval;
                    Response.success = true;
                    return Ok(Response);
                }
                Response.error = $"No Pages Found...!";
                return NotFound(Response);
            }
            catch (Exception ex)
            {
                Response.error = ex.Message;
                return StatusCode(500, Response);
            }
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            SCRMResponse Response = new();
            try
            {
                var posts = await _socialService.GetAllPosts();
                if (posts != null)
                {
                    Response.data = posts;
                    Response.success = true;
                    return Ok(Response);
                }
                Response.error = $"No Posts Found...!";
                return NotFound(Response);
            }
            catch (Exception ex)
            {
                Response.error = ex.Message;
                return StatusCode(500, Response);
            }
        }

        [HttpGet("GetPostsById/{PostId}")]
        public async Task<IActionResult> GetPostsId([FromRoute] int PostId)
        {
            SCRMResponse Response = new();
            try
            {
                var posts = await _socialService.GetPostsById(PostId);
                if (posts != null)
                {
                    Response.data = posts;
                    Response.success = true;
                    return Ok(Response);
                }
                Response.error = $"No Posts Found...!";
                return NotFound(Response);
            }
            catch (Exception ex)
            {
                Response.error = ex.Message;
                return StatusCode(500, Response);
            }
        }

        [HttpGet("GetPostsByPlateForm/{platformId}")]
        public async Task<IActionResult> GetPostsByPlateForm([FromRoute] int platformId)
        {
            SCRMResponse Response = new();
            try
            {
                var posts = await _socialService.GetPostsByPlateForm(platformId);
                if (posts != null)
                {
                    Response.data = posts;
                    Response.success = true;
                    return Ok(Response);
                }
                Response.error = $"No Posts Found...!";
                return NotFound(Response);
            }
            catch (Exception ex)
            {
                Response.error = ex.Message;
                return StatusCode(500, Response);
            }
        }

    }
}
