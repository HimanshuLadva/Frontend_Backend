using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSUserTemplatesRepository
    {
        Task<WCMSUserTemplates?> AddUserTemplateInfoAsync(WCMSUserTemplates userTemplateInfo);
        Task<bool> DeleteUserTemplateInfoByIdAsync(int id);
        Task<WCMSUserTemplates?> GetUserTemplateinfoByTemplateAsync(int TemplateId, string UserId);
        Task<List<WCMSUserTemplates>?> GetUserTemplatesAsync(string UserId);
    }
}