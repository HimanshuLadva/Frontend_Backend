using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    /// The WCMSTemplate repository Interface.
    /// </summary>
    public interface IWCMSTemplateRepository
    {
        /// <summary>
        /// Gets the all template.
        /// <para>
        ///     Gets the <see cref="string"/> hostInfo.
        /// </para>
        /// </summary>
        /// <param name="hostInfo">The Domain of host in string formate.</param>
        /// <returns>Returns <see cref="List{T}?"/> where T is <seealso cref="WCMSTemplatesModel"/></returns>
        List<WCMSTemplatesModel> GetAllTemplate(string hostInfo);
        /// <summary>
        /// Gets the all pages of perticular template.
        /// <para>
        ///     Gets the <see cref="int"/> templateId.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns <see cref="List{T}?"/>where T is <seealso cref="string"/></returns>
        List<string> GetAllPages(int templateId);
        /// <summary>
        /// Gets the all template fields of perticular template by mapped with pages.
        /// <para>
        ///     Gets the <see cref="int"/> templateId.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns <see cref="List{T}?"/>where T is <seealso cref="Dictionary{TKey, TValue}"/> where TKey is <see cref="string?"/> and TValue is <see cref="List{T}?"/> where T is <see cref="WCMSTemplatePageFieldsModel?"/></returns>
        List<KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>> GetAllTemplateFields(int templateId);
        /// <summary>
        /// Gets the meta fields of perticular template pagewise.
        /// <para>
        ///     Gets the <see cref="int"/> templateId.
        /// </para>
        /// </summary>
        /// <param name="templateId">The tmplate id.</param>
        /// <returns>Returns <see cref="List{T}?"/>where T is <seealso cref="WCMSTemplatePageFieldsModel"/></returns>
        List<WCMSTemplatePageFieldsModel> GetMetaFields(int templateId);
        /// <summary>
        /// Stores the images.
        /// <para>
        ///     Gets the <see cref="IFormFile"/> file.
        /// </para>
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Returns <see cref="string"/> A path to be store in database.</returns>
        Task<string> StoreImages(IFormFile file);
        /// <summary>
        /// Saves the template info.
        /// <para>
        ///     Gets the <see cref="Array"/> of <see cref="WCMSTemplatePageFieldsModel"/> with fields values.
        /// </para>
        /// <para>
        ///     Gets the <see cref="int"/> templateId.
        /// </para>
        /// <para>
        ///     Gets the <see cref="Array"/> of <see cref="string"/> file path.
        /// </para>
        /// </summary>
        /// <param name="infoModel">The info model with values of fields.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="files">The files.</param>
        /// <returns>Returns <see cref="int"/> The preview Id stored in database.</returns>
        int SaveTemplateInfo(WCMSTemplatePageFieldsModel[] infoModel, int templateId, string[] files);
        /// <summary>
        /// Download zip of the edited template.
        /// <para>
        ///     Gets the <see cref="int"/> Id of User.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> folder name where edited template is stored.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="folderName">The folder name.</param>
        /// <returns>Returns <see cref="FileContentResult"/> of Type Bytes of array which is read from zip. </returns>
        FileContentResult Download(int userId, string folderName);
        /// <summary>
        /// Deletes the folderand zip.
        /// <para>
        ///     Gets the <see cref="string"/> path where zip and edited template stored. 
        /// </para>
        /// </summary>
        /// <param name="path">The path.</param>
        void DeleteFolderandZip(string path);
        /// <summary>
        /// Copies all files and folders from directory to destinationDir.
        /// <para>
        ///     Gets the <see cref="DirectoryInfo"/> directory info from which files and folders will be copied.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> destinationDir path where files and folders of <see cref="DirectoryInfo"/> will be stored.
        /// </para>
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="destinationDir">The destination dir.</param>
        void CopyTemplate(DirectoryInfo directory, string destinationDir);
        /// <summary>
        /// Previews the template.
        /// <para>
        ///     Gets the <see cref="int"/> preview Id of Template related to User.
        /// </para>
        /// </summary>
        /// <param name="previewId">The preview id.</param>
        /// <returns> Returns <see cref="string"/> Url of edited template which is to be displayed.</returns>
        string PreviewTemplate(int previewId);
        /// <summary>
        /// Defaults the preview template.
        /// <para>
        ///     Gets the <see cref="int"/> Id of template which is to be preview.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns <see cref="string"/> Url of default edited template which is to be displayed.</returns>
        string DefaultPreviewTemplate(int templateId);
        /// <summary>
        /// Edits the template.
        /// <para>
        ///     Gets the <see cref="int"/> preview Id of Template related to User.
        /// </para>
        /// </summary>
        /// <param name="previewId">The preview Id.</param>
        /// <returns>Returns <see cref="string"/> folder name where edited template stored. </returns>
        string EditTemplate(int previewId);
        /// <summary>
        /// Deletes the template info.
        /// </summary>
        void DeleteTemplateInfo();
        /// <summary>
        /// Gets the globle settings which includes 
        /// <list type="bullet"> <see cref="List{T}"/> where T is <see cref="WCMSTemplatesModel"/>.</list>
        /// <list type="bullet"> <see cref="WCMSFieldsMasterModel"/> for GA Tag.</list>
        /// <list type="bullet"> <see cref="WCMSFieldsMasterModel"/> for Facebook Pixel.</list>
        /// <list type="bullet"> <see cref="SocialChannelModel"/> for social media accounts. </list>
        /// </summary>
        /// <returns>Returns <see cref="WCMSGlobleSettingsViewModel"/></returns>
        WCMSGlobleSettingsViewModel GetGlobleSettings();
        /// <summary>
        /// Gets the colors and fonts by template Id.
        /// <para>
        ///     Gets the <see cref="int"/> Id of template.
        /// </para>
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Returns <see cref="WCMSColorsAndFontsModel"/></returns>
        WCMSColorsAndFontsModel GetColorsAndFontsByTemplateId(int templateId);
        /// <summary>
        /// Saves the general settings in database.
        /// <para>
        ///     Gets the <see cref="WCMSGlobleSettingsModel"/>.
        /// </para>
        /// </summary>
        /// <param name="globleSettingsModel">The globle settings model.</param>
        /// <returns>Returns <see cref="int"/> The preview Id stored in database which has relation of template with User.</returns>
        int SaveGeneralSettings(WCMSGlobleSettingsModel globleSettingsModel);
    }
}