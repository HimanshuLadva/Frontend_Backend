using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSTemplatesRepository
    {
        /// <summary>
        ///     A synchoronous method to Get all the Records of WCMSTemplate that exists on the Database.
        /// </summary>
        /// <returns>Returns a <see cref="List{T}"/> of objects of Type <see cref="WCMSTemplates"/></returns>
        List<WCMSTemplates> GetAllTemplatesAsync();
    }
}