using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSTemplatePageRepository
    {
        List<WCMSTemplatePages> GetAllTemplatePages();
    }
}