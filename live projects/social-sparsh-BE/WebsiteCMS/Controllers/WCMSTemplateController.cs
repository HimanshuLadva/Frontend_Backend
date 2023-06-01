using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.Controllers
{
    /// <summary>
    /// The WCMSTemplate controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WCMSTemplateController : ControllerBase
    {
        private readonly IWCMSTemplateRepository _templateRepository;
        private readonly IWCMSTemplatesService _templateService;
        private readonly IAWSImageService _imageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WCMSTemplateController"/> class.
        /// </summary>
        /// <param name="templateRepository">The template repository.</param>
        /// <param name="templateService">The template service.</param>
        public WCMSTemplateController(IWCMSTemplateRepository templateRepository, IWCMSTemplatesService templateService, IAWSImageService imageService)
        {
            _templateRepository = templateRepository;
            _templateService = templateService;
            _imageService = imageService;
        }

        /// <summary>
        /// Gets the all templates from database.
        /// </summary>
        /// <returns>Returns a Http Response that contains the <see cref="List{T}"/> where T is <seealso cref="WCMSTemplatesModel"/> of template objects.</returns>
        [HttpGet]
        public IActionResult GetAllTemplate()
        {
            WCMSResponse res = new();
            try
            {
                var baseURL = _imageService.GetImageBaseUrl();
                var template = _templateService.GetAllTemplateAsync(baseURL);
                if (template != null)
                {
                    ResponseMetadata<object> metadata = new()
                    {
                        records = template
                    };
                    res.data = metadata;
                    res.Success = true;
                    return Ok(res);
                }
                res.data = "No templates available";
                res.Success = true;
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Gets the all template fields by Template Id.
        /// <para>
        ///     Gets the <see cref="int"/> Id of template.
        /// </para>
        /// </summary>
        /// <param name="templateId">The Template Id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="FieldsRespose"/> array of template pages string and array of Template fields dictionary.</returns>
        [HttpGet("{templateId}/Fields")]
        public IActionResult GetAllTemplateFieldsByID([FromRoute] int templateId)
        {
            WCMSResponse res = new();
            try
            {
                var Pages = _templateRepository.GetAllPages(templateId);
                var TemplateFields = _templateRepository.GetAllTemplateFields(templateId);

                FieldsRespose respose = new()
                {
                    Pages = Pages ?? null,
                    Fields = TemplateFields ?? null
                };

                if (respose != null)
                {
                    res.data = respose;
                    res.Success = true;
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Saves the images into amazon S3.
        /// </summary>
        /// <para>
        ///     Gets the <see cref="IFormFile"/> file.
        /// </para>
        /// <param name="file">The file.</param>
        /// <returns>Returns a Http Response that contains the <see cref="string"/> of image path where which is to be store into database.</returns>
        [HttpPost("files")]
        public async Task<IActionResult> SaveImagesInfo(IFormFile file)
        {
            WCMSResponse res = new();
            try
            {
                var img = await _templateRepository.StoreImages(file);
                if (img != null)
                {
                    ResponseMetadata<object> metadata = new()
                    {
                        records = img
                    };
                    res.data = metadata;
                    res.Success = true;
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Saves the template info.
        /// <para>
        ///     Gets the <see cref="WCMSRequest"/> request data which contains array of image path <see cref="string"/> and objects of  <see cref="WCMSTemplatePageFieldsModel"/> with values.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> Id of the template.
        /// </para>
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="int"/> Id which is related between user and template.</returns>
        [HttpPost("{templateId}")]
        public IActionResult SaveTemplateInfo([FromBody] WCMSRequest requestData, [FromRoute] int templateId)
        {
            WCMSResponse res = new();
            try
            {
                var previewId = _templateRepository.SaveTemplateInfo(requestData.data!, templateId, requestData.files!);

                ResponseMetadata<object> metadata = new()
                {
                    records = previewId
                };
                res.data = metadata;
                res.Success = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Downloads the file.
        /// <para>
        ///     Gets the <see cref="int"/> Id which is related between user and template.
        /// </para>
        /// </summary>
        /// <param name="userTemplateId">The user template id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="FileContentResult"/> of zip to download.</returns>
        [HttpGet("{userTemplateId}/Download")]
        public IActionResult DownloadFile([FromRoute] int userTemplateId)
        {
            WCMSResponse res = new();
            try
            {
                string folderName = _templateRepository.EditTemplate(userTemplateId);
                var result = _templateRepository.Download(userTemplateId, folderName);

                if (result != null)
                {
                    ResponseMetadata<object> metadata = new()
                    {
                        records = result
                    };
                    res.data = metadata;
                    res.Success = true;
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Previews the template.
        /// <para>
        ///     Gets the <see cref="int"/> Id which is related between user and template.
        /// </para>
        /// </summary>
        /// <param name="previewId">The preview id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="string"/> of template url which is use to preview or display template.</returns>
        [HttpGet("{previewId}/Preview")]
        public IActionResult PreviewTemplate([FromRoute] int previewId)
        {
            WCMSResponse res = new();
            try
            {
                var s = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
                var previewUrl = _templateRepository.PreviewTemplate(previewId);
                var result = !string.IsNullOrEmpty(previewUrl) ? s + previewUrl : string.Empty;

                ResponseMetadata<object> metadata = new()
                {
                    records = result
                };
                res.data = metadata;
                res.Success = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Defaults the preview template.
        /// <para>
        ///     Gets the <see cref="int"/> Id of the template.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="string"/> of default template url which is use to preview or display template.</returns>
        [HttpGet("{templateId}/DefaultPreview")]
        public IActionResult DefaultPreviewTemplate([FromRoute] int templateId)
        {
            WCMSResponse res = new();
            try
            {
                var s = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value;
                var previewUrl = _templateRepository.DefaultPreviewTemplate(templateId);
                var result = !string.IsNullOrEmpty(previewUrl) ? s + previewUrl : string.Empty;

                ResponseMetadata<object> metadata = new()
                {
                    records = result
                };
                res.data = metadata;
                res.Success = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Deletes the template info of the perticular User.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpDelete]
        public IActionResult DeleteTemplateInfo()
        {
            WCMSResponse res = new();
            try
            {
                _templateRepository.DeleteTemplateInfo();

                ResponseMetadata<object> metadata = new()
                {
                    records = "Info Deleted Successfully"
                };
                res.data = metadata;
                res.Success = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Gets the globle settings which includes
        ///  <list type="bullet"> <see cref="List{T}"/> where T is <see cref="WCMSTemplatesModel"/>.</list>
        ///  <list type="bullet"> <see cref="WCMSFieldsMasterModel"/> for GA Tag.</list>
        ///  <list type="bullet"> <see cref="WCMSFieldsMasterModel"/> for Facebook Pixel.</list>
        ///  <list type="bullet"> <see cref="SocialChannelModel"/> for social media accounts. </list>
        /// </summary>
        /// <returns>Returns a Http Response that contains the <see cref="WCMSGlobleSettingsViewModel"/></returns>
        [HttpGet("GetGeneralSettings")]
        public IActionResult GetGlobleSettings()
        {
            WCMSResponse res = new();
            try
            {
                var glble = _templateRepository.GetGlobleSettings();

                ResponseMetadata<object> metadata = new()
                {
                    records = glble
                };
                res.data = metadata;
                res.Success = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Gets the colors and fonts by template id.
        /// <para>
        ///     Gets the <see cref="int"/> Id of the template.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns a Http Response that contains the <see cref="WCMSColorsAndFontsModel"/> of image path where which is to be store into database.</returns>
        [HttpGet("GetColorsAndFonts/{templateId}")]
        public IActionResult GetColorsAndFontsByTemplateId([FromRoute] int templateId)
        {
            WCMSResponse res = new();
            try
            {
                var FontsAndColors = _templateRepository.GetColorsAndFontsByTemplateId(templateId);
                if (FontsAndColors != null)
                {
                    res.data = FontsAndColors;
                    res.Success = true;
                    return Ok(res);
                }
                res.data = "No Fonts and Colors found for this Template";
                res.Success = true;
                return NotFound(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Saves the general settings.
        /// <para>
        ///     Gets the <see cref="WCMSGlobleSettingsModel"/> page name of the template.
        /// </para>
        /// </summary>
        /// <param name="globleSettingsModel">The globle settings model.</param>
        /// <returns>Returns a Http Response that contains the <see cref="int"/> Preview Id stored in database which has relation of template with User.</returns>
        [HttpPost("SaveGeneralSettings")]
        public IActionResult SaveGeneralSettings([FromBody] WCMSGlobleSettingsModel globleSettingsModel)
        {
            WCMSResponse res = new();
            try
            {
                var FontsAndColors = _templateRepository.SaveGeneralSettings(globleSettingsModel);
                if (FontsAndColors != 0)
                {
                    res.data = FontsAndColors;
                    res.Success = true;
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.error = ex.Message;
                return StatusCode(500, res);
            }
        }
    }
}