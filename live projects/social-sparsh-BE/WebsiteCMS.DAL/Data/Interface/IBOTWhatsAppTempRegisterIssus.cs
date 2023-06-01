using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTWhatsAppTempRegisterIssus
    {
        Task<BOTWhatsAppTemplateRegisterIssue> AddIssue(BOTWhatsAppTemplateRegisterIssue model);
    }
}