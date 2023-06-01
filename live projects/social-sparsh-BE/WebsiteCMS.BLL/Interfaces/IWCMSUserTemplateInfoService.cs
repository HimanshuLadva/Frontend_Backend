using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWCMSUserTemplateInfoService
    {
        Task<WCMSResponse> AddUserTemplateInfoAsync(WCMSUserTemplatesModel model);
        Task<WCMSResponse> DeleteUserTemplateInfoByIdAsync(int id);
        Task<WCMSResponse> GetUserTemplateInfoByIdAsync(int id);
    }
}