using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTWhatsAppTemplateRepository
    {
        Task<BOTWhatsAppTemplates> AddWhatsappTemplate(BOTWhatsAppTemplates model);
        Task<bool> DeleteWhatsAppTemplate(long id);
        Task<List<BOTWhatsAppTemplates>> GetAllWhatsAppTemplates();
        Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByIdAsync(long id);
        Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByQueID(long id);
        Task<BOTWhatsAppTemplates?> GetWhatsAppTemplatesByTempID(string id);
        Task<BOTWhatsAppTemplates?> UpdateWhatsAppTemplate(BOTWhatsAppTemplates model);
    }
}