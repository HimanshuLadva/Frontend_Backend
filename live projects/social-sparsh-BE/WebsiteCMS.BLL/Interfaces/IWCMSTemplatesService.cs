using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWCMSTemplatesService
    {
        List<WCMSTemplatesModel> GetAllTemplateAsync(string HostInfo);

    }
}