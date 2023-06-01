using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWCMSTemplateFieldsRepository
    {
        List<WCMSTemplatePageFields> GetAllTemplateFields(int templatePageId);
    }
}